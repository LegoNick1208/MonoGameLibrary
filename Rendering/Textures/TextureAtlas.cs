using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Rendering.Textures;

public class TextureAtlas
{
    internal Dictionary<string, TextureRegion> _regions;

    internal Dictionary<string, Animation> _animations;
    public TextureAtlas()
    {
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }
    public TextureAtlas(Texture2D texture)
    {
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }
    public TextureAtlas(List<TextureAtlas> Atlas)
    {
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();

        foreach (TextureAtlas atlas in Atlas)
        {
            foreach (var kvp in atlas._regions)
            {
                if (_regions.ContainsKey(kvp.Key))
                    throw new InvalidOperationException($"Duplicate region name found: '{kvp.Key}'");
                _regions[kvp.Key] = kvp.Value;
            }
            foreach (var kvp in atlas._animations)
            {
                if (_animations.ContainsKey(kvp.Key))
                    throw new InvalidOperationException($"Duplicate animation name found: '{kvp.Key}'");
                _animations[kvp.Key] = kvp.Value;
            }
        }
    }
    private void AddRegion(Texture2D Texture, string name, int x, int y, int width, int height)
    {
        TextureRegion region = new TextureRegion(Texture, x, y, width, height);
        _regions.Add(name, region);
    }
    internal TextureRegion GetRegion(string name)
    {
        if(_regions.TryGetValue(name, out TextureRegion region))
        {
            return region;
        }
        throw new KeyNotFoundException($"Region '{name}' not found in the texture atlas.");
    }
    public static TextureAtlas FromFile(ContentManager content, string fileName)
    {
        TextureAtlas atlas = new TextureAtlas();

        string filePath = Path.Combine(content.RootDirectory, fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                string texturePath = root.Element("Texture").Value;
                Texture2D Texture = content.Load<Texture2D>(texturePath);
                var regions = root.Element("Regions")?.Elements("Region");

                if (regions != null)
                {
                    foreach (var region in regions)
                    {
                        string name = region.Attribute("name")?.Value;
                        int x = int.Parse(region.Attribute("x")?.Value ?? "0");
                        int y = int.Parse(region.Attribute("y")?.Value ?? "0");
                        int width = int.Parse(region.Attribute("width")?.Value ?? "0");
                        int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                        if (!string.IsNullOrEmpty(name))
                        {
                            atlas.AddRegion(Texture, name, x, y, width, height);
                        }
                    }
                }
                var animationElements = root.Element("Animations").Elements("Animation");

                if (animationElements != null)
                {
                    foreach (var animationElement in animationElements)
                    {
                        string name = animationElement.Attribute("name")?.Value;
                        float delayInMilliseconds = float.Parse(animationElement.Attribute("delay")?.Value ?? "0");
                        TimeSpan delay = TimeSpan.FromMilliseconds(delayInMilliseconds);

                        List<TextureRegion> frames = new List<TextureRegion>();

                        var frameElements = animationElement.Elements("Frame");

                        if (frameElements != null)
                        {
                            foreach (var frameElement in frameElements)
                            {
                                string regionName = frameElement.Attribute("region").Value;
                                TextureRegion region = atlas.GetRegion(regionName);
                                frames.Add(region);
                            }
                        }

                        Animation animation = new Animation(frames, delay);
                        atlas.AddAnimation(name, animation);
                    }
                }

                return atlas;
            }
        }
    }
    private void AddAnimation(string animationName, Animation animation)
    {
        _animations.Add(animationName, animation);
    }
}
