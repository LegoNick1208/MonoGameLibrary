using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using MonoGameLibrary.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse = MonoGameLibrary.Input.Mouse;

namespace MonoGameLibrary.Rendering.Objects
{
    public class Sprite : RenderObject
    {
        private TextureRegion TextureRegion { get; set; }
        public override float Width
        {
            get { return TextureRegion.Width; }
        }
        public override float Height
        {
            get { return TextureRegion.Height; }
        }
        public Sprite(SpriteConfig config) : base()
        {
            TextureRegion = Core.Atlas.GetRegion(config.TextureName);
            X = config.X;
            Y = config.Y;
            Rotation = config.Rotation;
            Origin = config.Origin;
            Scale = config.Scale;
            Tint = config.Tint;
            SpriteEffects = config.SpriteEffects;
            ZIndex = config.ZIndex;   
        }
        public override void Draw()
        {
            if(_Deleted || !Visible) return;
            Core.SpriteBatch.Draw
            (
                TextureRegion.Texture,
                new Vector2((float)Math.Round(X), (float)Math.Round(Y)),
                TextureRegion.SourceRectangle,
                Tint,
                Rotation,
                _OriginVec,
                Scale,
                SpriteEffects,
                (float) (ZIndex/100f)
            );
        }
    }
}
