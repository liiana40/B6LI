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
    public class Box
    {
        public static Texture2D Tex;
        Vector2 Position;
        Vector2 Velocity = new Vector2(4, 4);
        Rectangle CollisionRect = new Rectangle(0, 0, 16, 16);
        static Random rnd = new Random();
        int BoundX;
        int BoundY;
        bool isSelected = false;
        public static bool hasSelected = false;
        public float Speed = 5;
        public Box()
        {
            Position.X = rnd.Next(800 / CollisionRect.Width) * CollisionRect.Width;
            Position.Y = rnd.Next(480 / CollisionRect.Height) * CollisionRect.Height;
            BoundX = 800 - CollisionRect.Width;
            BoundY = 480 - CollisionRect.Height;
        }

        public void Update(List<Box> Boxes, KeyboardBox keyboardBox, KeyboardState ks, MouseBox mouseBox, MouseState ms, MouseState oms)
        {
            if (ms.LeftButton == ButtonState.Pressed && oms.LeftButton == ButtonState.Released)
            {
                
                if (CollisionRect.Contains(ms.X, ms.Y) && !hasSelected)
                {
                    
                    isSelected = !isSelected; //true
                }
            }
            if (isSelected)
            {
                Vector2 diff = new Vector2(ms.X - Position.X, ms.Y - Position.Y);
                if (diff.LengthSquared() > 36)
                {
                    diff.Normalize();
                    diff *= 6;
                    Position += diff;
                }
            }
            else
            Position += Velocity;
            if (Position.X < 0)
            {
                Position.X = 0;
                Velocity.X = Math.Abs(Velocity.X);
            }
            if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y = Math.Abs(Velocity.Y);
            }
            if (Position.X > BoundX)
            {
                Position.X = BoundX;
                Velocity.X = -Math.Abs(Velocity.X);
            }
            if (Position.Y > BoundY)
            {
                Position.Y = BoundY;
                Velocity.Y = -Math.Abs(Velocity.Y);
            }
            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;


            foreach (var box in Boxes)
            {
                //Box list collision
                if (box == this) continue;
                if (box.CollisionRect.Intersects(CollisionRect))
                {
                    Rectangle tmp = Rectangle.Intersect(box.CollisionRect, CollisionRect);
                    if (tmp.Width > tmp.Height)
                    {
                        Position.Y -= tmp.Height * .5f * Math.Sign(box.CollisionRect.Y - CollisionRect.Y);
                        Velocity.Y *= -1;
                    }
                    else
                    {
                        Position.X -= tmp.Width * .5f * Math.Sign(box.CollisionRect.X - CollisionRect.X);
                        Velocity.X *= -1;
                    }
                }
            }
            //mouseBox collision
            if (mouseBox != null && mouseBox.CollisionRect.Intersects(CollisionRect))
            {
                Rectangle tmp = Rectangle.Intersect(mouseBox.CollisionRect, CollisionRect);
                if (tmp.Width > tmp.Height)
                {
                    Position.Y -= tmp.Height* Math.Sign(mouseBox.CollisionRect.Y - CollisionRect.Y);
                    Velocity.Y *= -1;
                }
                else
                {
                    Position.X -= tmp.Width * Math.Sign(mouseBox.CollisionRect.X - CollisionRect.X);
                    Velocity.X *= -1;
                }
            }

            //keyboardBox collision
            if (keyboardBox.CollisionRect.Intersects(CollisionRect))
            {
                if (ks.IsKeyDown(Keys.Down)) Position.Y += Speed;
                if (ks.IsKeyDown(Keys.Up)) Position.Y -= Speed;
                if (ks.IsKeyDown(Keys.Right)) Position.X += Speed;
                if (ks.IsKeyDown(Keys.Left)) Position.X -= Speed;

                Rectangle tmp = Rectangle.Intersect(keyboardBox.CollisionRect, CollisionRect);
                if (tmp.Width > tmp.Height)
                {
                    Position.Y -= tmp.Height * Math.Sign(keyboardBox.CollisionRect.Y - CollisionRect.Y);
                    Velocity.Y *= -1;
                }
                else
                {
                    Position.X -= tmp.Width * Math.Sign(keyboardBox.CollisionRect.X - CollisionRect.X);
                    Velocity.X *= -1;
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, CollisionRect, Color.White);
        
        }
    }
}
