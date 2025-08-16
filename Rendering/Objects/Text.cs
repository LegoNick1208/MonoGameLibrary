using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Rendering.Objects
{
    public class Text : RenderObject
    {
        public override float Width
        {
            get { return Font.MeasureString(Value).X; }
        }
        public override float Height
        {
            get { return Font.MeasureString(Value).Y; }
        }
        private SpriteFont Font;
        public String Value { get; set; } = string.Empty;
        public Color Color
        {
            get { return Tint; }
            set { Tint = value; }
        }
        public Text(TextConfig config) : base()
        {
            Font = Core.Content.Load<SpriteFont>(config.FontPath);
            Value = config.Value;
            X = config.X;
            Y = config.Y;
            Rotation = config.Rotation;
            Origin = config.Origin;
            Scale = config.Scale;
            Color = config.Color;
            SpriteEffects = config.SpriteEffects;
            ZIndex = config.ZIndex;
            Visible = config.Visible;
        }
        public override void Draw()
        {
            if (_Deleted || !Visible) return;
            Core.SpriteBatch.DrawString
            (
                Font,
                Value,
                new Vector2((float)Math.Round(X),(float)Math.Round(Y)),
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
