using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace B6LI
{
    public class MouseBox
    {
        public static Texture2D Tex;
        Vector2 Position;
        public Rectangle CollisionRect = new Rectangle(0, 0, 32, 32);

        public void Update(MouseState ms)
        {
            Position.X = ms.X;
            Position.Y = ms.Y;

            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, Position, Color.Red);
        }
    }
}
