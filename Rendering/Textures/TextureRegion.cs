using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Rendering.Textures;

public class TextureRegion
{
    public Texture2D Texture { get; private set; }
    public Rectangle SourceRectangle { get; private set; }
    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;
    public TextureRegion(Texture2D texture, Rectangle sourceRect)
    {
        Texture = texture;
        SourceRectangle = sourceRect;
    }
    public TextureRegion(Texture2D texture, int x, int y, int width, int height)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(x, y, width, height);
    }
}
