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
    public class KeyboardBox
    {
        public static Texture2D Tex;
        Vector2 Position;
        public float Speed = 5;
        public Rectangle CollisionRect = new Rectangle(0, 0, 32, 32);
        static Random rnd = new Random();
        int BoundX;
        int BoundY;

        public KeyboardBox()
        {
            Position.X = rnd.Next(800 / CollisionRect.Width) * CollisionRect.Width;
            Position.Y = rnd.Next(480 / CollisionRect.Height) * CollisionRect.Height;
            BoundX = 800 - CollisionRect.Width;
            BoundY = 480 - CollisionRect.Height;
        }

        public void Update(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Down)) Position.Y += Speed;
            if (ks.IsKeyDown(Keys.Up)) Position.Y -= Speed;
            if (ks.IsKeyDown(Keys.Right)) Position.X += Speed;
            if (ks.IsKeyDown(Keys.Left)) Position.X -= Speed;

            if (Position.X < 0)
                Position.X = 0;
            if (Position.Y < 0)
                Position.Y = 0;
            if (Position.X > BoundX)
                Position.X = BoundX;
            if (Position.Y > BoundY)
                Position.Y = BoundY;

            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, CollisionRect, Color.Blue);
        }
    }
}
