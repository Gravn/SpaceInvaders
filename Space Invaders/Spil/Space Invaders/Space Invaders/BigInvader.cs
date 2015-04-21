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
        public BigInvader(Vector2 position, float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            LoadContent();
        }

        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            //currentIndex will be 2 when hit.
            if (currentIndex != 1)
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

        public override void OnCollision(GameObject other)
        {
            if (other is PlayerProjectile)
            {
                Player.canShoot = true;
                currentIndex = 1;
                Destroy(other);
            }
        }
    }     
}
