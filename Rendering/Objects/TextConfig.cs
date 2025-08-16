using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Rendering.Objects
{
    public class TextConfig
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public string Value { get; set; } = string.Empty;
        public string FontPath { get; set; } = string.Empty;
        public float Rotation { get; set; } = 0f;
        public SpriteOrigin Origin { get; set; } = SpriteOrigin.Center;
        public Vector2 Scale { get; set; } = new Vector2(1, 1);
        public Color Color { get; set; } = Color.White;
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        public int ZIndex { get; set; } = 0;
        public bool Visible { get; set; } = true;
    }
}
