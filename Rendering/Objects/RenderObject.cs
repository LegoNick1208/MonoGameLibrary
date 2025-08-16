using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using MonoGameLibrary.Rendering.Objects;
using MonoGameLibrary.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse = MonoGameLibrary.Input.Mouse;

namespace MonoGameLibrary.Rendering
{
    public abstract partial class RenderObject : IRenderable
    {
        protected bool _Deleted { get; set; } = false;
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Rotation { get; set; } = 0f;
        public Color Tint { get; set; } = Color.White;
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        public bool Visible { get; set; } = true;
        protected int _ZIndex { get; set; } = 0;
        public virtual float Width { get; set; }
        public virtual float Height { get; set; }
        public int ZIndex
        {
            get { return _ZIndex; }
            set
            {
                _ZIndex = value;
                if (_ZIndex < 0)
                {
                    _ZIndex = 0;
                }
                else if (_ZIndex > 100)
                {
                    _ZIndex = 100;
                }
            }
        }
        public SpriteOrigin Origin
        {
            get { return _OriginEnum; }
            set
            {
                _OriginEnum = value;
                switch (value)
                {
                    case SpriteOrigin.TopLeft:
                        _OriginVec = new Vector2(0, 0);
                        break;
                    case SpriteOrigin.TopRight:
                        _OriginVec = new Vector2(Width, 0);
                        break;
                    case SpriteOrigin.BottomLeft:
                        _OriginVec = new Vector2(0, Height);
                        break;
                    case SpriteOrigin.BottomRight:
                        _OriginVec = new Vector2(Width, Height);
                        break;
                    case SpriteOrigin.Center:
                        _OriginVec = new Vector2(Width / 2f, Height / 2f);
                        break;
                }
            }
        }
        protected SpriteOrigin _OriginEnum = SpriteOrigin.Center;
        protected Vector2 _OriginVec = new Vector2(0, 0);
        protected Vector2 Scale = new Vector2(0, 0);
        public RenderObject()
        {
            RenderingManager.Add(this);
        }
        ~RenderObject()
        {
            this.Delete();
        }
        public void Delete()
        {
            if (_Deleted) return;
            _Deleted = true;
            RenderingManager.Remove(this);
        }
        public bool IsClicked(MouseButton button = MouseButton.Left, bool Pressed = true)
        {
            if (_Deleted || !Visible) return false;
            if (Pressed)
            {
                if (!Mouse.IsButtonPressed(button)) return false;
            }
            else
            {
                if (!Mouse.IsButtonDown(button)) return false;
            }
            return IsMouseOver();
        }
        public bool IsMouseOver()
        {
            if (_Deleted || !Visible) return false;
            float originX = 0f, originY = 0f;
            switch (Origin)
            {
                case SpriteOrigin.TopLeft:
                    originX = 0; originY = 0;
                    break;
                case SpriteOrigin.TopRight:
                    originX = Width; originY = 0;
                    break;
                case SpriteOrigin.BottomLeft:
                    originX = 0; originY = Height;
                    break;
                case SpriteOrigin.BottomRight:
                    originX = Width; originY = Height;
                    break;
                case SpriteOrigin.Center:
                    originX = Width / 2f; originY = Height / 2f;
                    break;
            }
            float scaledOriginX = originX * Scale.X;
            float scaledOriginY = originY * Scale.Y;
            float localX = Mouse.X - X;
            float localY = Mouse.Y - Y;
            float cos = (float)Math.Cos(-Rotation);
            float sin = (float)Math.Sin(-Rotation);
            float rotatedX = localX * cos - localY * sin;
            float rotatedY = localX * sin + localY * cos;
            rotatedX += scaledOriginX;
            rotatedY += scaledOriginY;
            float scaledWidth = Width * Scale.X;
            float scaledHeight = Height * Scale.Y;

            return rotatedX >= 0 && rotatedX <= scaledWidth &&
                   rotatedY >= 0 && rotatedY <= scaledHeight;
        }
        public void Show()
        {
            Visible = true;
        }
        public void Hide()
        {
            Visible = false;
        }
        public abstract void Draw();
    }
}
