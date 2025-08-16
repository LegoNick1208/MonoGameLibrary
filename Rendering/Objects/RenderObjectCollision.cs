using Microsoft.Xna.Framework;
using MonoGameLibrary.Rendering.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Rendering
{
    public abstract partial class RenderObject
    {
        public bool CollidesWith(RenderObject other)
        {
            // 1. Get transformed corners of both objects
            Vector2[] aCorners = GetTransformedCorners();
            Vector2[] bCorners = other.GetTransformedCorners();

            // 2. Test both shapes for separation on all axes (SAT)
            if (HasSeparatingAxis(aCorners, bCorners)) return false;
            if (HasSeparatingAxis(bCorners, aCorners)) return false;

            return true; // No separating axis found = collision
        }

        private Vector2[] GetTransformedCorners()
        {
            // Base half-width/height
            float w = Width * Scale.X;
            float h = Height * Scale.Y;

            // Apply origin offset
            Vector2 originOffset = Origin switch
            {
                SpriteOrigin.TopLeft => new Vector2(0, 0),
                SpriteOrigin.TopRight => new Vector2(w, 0),
                SpriteOrigin.BottomLeft => new Vector2(0, h),
                SpriteOrigin.BottomRight => new Vector2(w, h),
                SpriteOrigin.Center => new Vector2(w / 2f, h / 2f),
                _ => Vector2.Zero
            };

            // Define local corners
            Vector2[] corners = new Vector2[]
            {
        new Vector2(0, 0) - originOffset,     // top-left
        new Vector2(w, 0) - originOffset,     // top-right
        new Vector2(w, h) - originOffset,     // bottom-right
        new Vector2(0, h) - originOffset      // bottom-left
            };

            // Rotate + translate to world space
            float cos = MathF.Cos(Rotation);
            float sin = MathF.Sin(Rotation);
            for (int i = 0; i < 4; i++)
            {
                float rx = corners[i].X * cos - corners[i].Y * sin;
                float ry = corners[i].X * sin + corners[i].Y * cos;
                corners[i] = new Vector2(X + rx, Y + ry);
            }

            return corners;
        }

        private bool HasSeparatingAxis(Vector2[] polyA, Vector2[] polyB)
        {
            for (int i = 0; i < polyA.Length; i++)
            {
                // Get the current edge
                Vector2 p1 = polyA[i];
                Vector2 p2 = polyA[(i + 1) % polyA.Length];

                // Get perpendicular axis
                Vector2 axis = new Vector2(-(p2.Y - p1.Y), p2.X - p1.X);
                axis = Vector2.Normalize(axis);

                // Project both polygons onto axis
                GetProjection(polyA, axis, out float minA, out float maxA);
                GetProjection(polyB, axis, out float minB, out float maxB);

                // Check for gap
                if (maxA < minB || maxB < minA)
                    return true; // Separating axis found
            }
            return false;
        }

        private void GetProjection(Vector2[] polygon, Vector2 axis, out float min, out float max)
        {
            float dot = Vector2.Dot(polygon[0], axis);
            min = max = dot;

            for (int i = 1; i < polygon.Length; i++)
            {
                dot = Vector2.Dot(polygon[i], axis);
                if (dot < min) min = dot;
                else if (dot > max) max = dot;
            }
        }

    }
}
