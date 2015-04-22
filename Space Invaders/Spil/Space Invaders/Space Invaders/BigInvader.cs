using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class BigInvader : GameObject
    {
        Random random = new Random();
        Vector2[] shootingPosition = new Vector2[3];
        private int scoreValue = 100;

        public BigInvader(Vector2 position, float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            this.position = new Vector2(256, 0);
            this.direction = new Vector2(1, 0);
            LoadContent();
        }

        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            //currentIndex will be 1 when hit.
            if (currentIndex != 1)
            {
                if (position.X > 512)
                {
                    direction = new Vector2(-1, 0);
                    for (int i = 0; i < shootingPosition.Length; i++)
                    {
                        shootingPosition[i] = new Vector2(random.Next(0, 256), 0);
                    } 
                }
                if (position.X < -256)
                {
                    direction = new Vector2(1, 0);
                    for (int i = 0; i < shootingPosition.Length; i++)
                    {
                        shootingPosition[i] = new Vector2(random.Next(0, 256), 0);
                    } 
                }

                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                position += (direction * deltaTime * movementSpeed);

                for (int i = 0; i < shootingPosition.Length; i++)
                {
                    if ((int)position.X == (int)shootingPosition[i].X)
                    {
                        Shoot();
                    }   
                }

                base.Update(gameTime);

            }
            else
            {
                //this controls how long the 2nd frame(explosion) will be shown before destroying the object.
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= 0.3f)
                {
                    Destroy(this);
                }
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is PlayerProjectile)
            {
                Game1.score += scoreValue;
                Player.canShoot = true;
                currentIndex = 1;
                Destroy(other);
            }
        }

        private void Shoot()
        {
            GameObject bigShot = Game1.obj_missile.Clone();
            bigShot.Position = new Vector2(this.position.X + 6, this.position.Y + 3);
            bigShot.MovementSpeed = 200;
            Game1.Objects.Add(bigShot);
        }
    }     
}
