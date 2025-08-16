using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGameLibrary.Rendering.Objects
{
    public class FilledRectangle : RenderObject
    {
        Texture2D Pixel;
        public override float Width { get; set; } = 100;
        public override float Height { get; set; } = 100;
        public Color Color
        {
            get { return Tint; }
            set { Tint = value; }
        }
        public FilledRectangle(FilledRectangleConfig config) : base()
        {
            Pixel = new Texture2D(Core.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
            Width = config.Width;
            Height = config.Height;
            X = config.X;
            Y = config.Y;
            Rotation = config.Rotation;
            Origin = config.Origin;
            Scale = config.Scale;
            Color = config.Color;
            SpriteEffects = config.SpriteEffects;
            ZIndex = config.ZIndex;
        }
        public override void Draw()
        {
            if (_Deleted || !Visible) return;
            Core.SpriteBatch.Draw
            (
                Pixel, 
                new Vector2((float)Math.Round(X),(float)Math.Round(Y)),
                new Rectangle((int)X, (int)Y, (int)Width, (int)Height),
                Color,
                Rotation,
                _OriginVec,
                Scale,
                SpriteEffects,
                (float)(ZIndex / 100f)
            );

        }
    }
}
