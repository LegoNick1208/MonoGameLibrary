using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Rendering;
using MonoGameLibrary.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary
{
    public class Core : Game
    {
        public static Core Instance { get; private set; }
        public static TextureAtlas Atlas { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static new GraphicsDevice GraphicsDevice { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public static new ContentManager Content { get; private set; }
        public static int MaxX => GraphicsDevice.Viewport.Width;
        public static int MaxY => GraphicsDevice.Viewport.Height;
        public Core() : this(new CoreConfig()) {}
        public Core(CoreConfig config)
        {
            if (Instance != null) throw new InvalidOperationException($"Only a single Core instance can be created");
            Instance = this;

            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = config.Width;
            Graphics.PreferredBackBufferHeight = config.Height;
            Graphics.IsFullScreen = config.FullScreen;
            Graphics.ApplyChanges();

            Window.Title = config.Title;

            Content = base.Content;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }
        public static void LoadAtlas(string[] AtlasFiles)
        {
            List<TextureAtlas> atlases = new List<TextureAtlas>();
            foreach (var file in AtlasFiles)
            {
                TextureAtlas fileAtlas = TextureAtlas.FromFile(Content, file);
                atlases.Add(fileAtlas);
            }
            Atlas = new TextureAtlas(atlases);
        }
        protected override void Initialize()
        {
            base.Initialize();
            GraphicsDevice = base.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            Keyboard.Update();
            Mouse.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            RenderingManager.Draw();
            base.Draw(gameTime);
        }
    }

}
