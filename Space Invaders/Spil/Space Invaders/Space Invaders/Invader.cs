using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Invader : GameObject
    {
        public static bool canShoot = false;
        private Point arrayPos = Point.Zero;



        public Point ArrayPos
        {
            get { return arrayPos; }
            set { arrayPos = value;}
        }

        public float CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }

        public Invader(Vector2 position,float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed,animationSpeed, sprite, frames)
        {
            LoadContent();
        }

        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            //currentIndex will be 2 when hit.
            if (currentIndex != 2)
            {
                base.Update(gameTime);
            }
            else
            {
                //this controls how long the 3rd frame(explosion) will be shown before destroying the object.
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= 0.3f)
                {
                    Destroy(this);
                }
            }
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            //float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //currentIndex += deltaTime * animationSpeed;
            //if (collisionBox.Right > 10)
            //{

            //}
            //if (currentIndex >= rectangles.Length - 1)
            //{
            //    currentIndex = 0;
            //}
            base.UpdateAnimation(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Projectile)
            {
                Game1.invaders[ArrayPos.X,ArrayPos.Y] = null;
                CurrentIndex = 2;
                Player.canShoot = true;
                Destroy(other);
            }

            if (other is Player)
            { 
                
            }
        }

    }
}
