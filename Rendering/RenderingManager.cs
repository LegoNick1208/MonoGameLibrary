using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Rendering
{
    internal static class RenderingManager
    {
        private static List<IRenderable> Objects = new List<IRenderable>();
        internal static void Add(IRenderable obj)
        {
            Objects.Add(obj);
        }
        internal static void Remove(IRenderable obj)
        {
            Objects.Remove(obj);
        }
        internal static void Draw()
        {
            Core.GraphicsDevice.Clear(Color.White);
            Core.SpriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            foreach (var obj in Objects)
            {
                obj.Draw();
            }
            Core.SpriteBatch.End();
        }
    }
}
