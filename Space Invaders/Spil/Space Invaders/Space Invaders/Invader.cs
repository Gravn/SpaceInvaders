﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Space_Invaders
{
    class Invader : GameObject
    {
        private Point arrayPos = Point.Zero;
        private int scoreValue = 10;

        public Point ArrayPos
        {
            get { return arrayPos; }
            set { arrayPos = value;}
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

        public override void OnCollision(GameObject other)
        {
            if (other is PlayerProjectile)
            {
                SoundEffectInstance invaderExplosion = Game1.explosionSound.CreateInstance();
                invaderExplosion.IsLooped = false;
                invaderExplosion.Volume = 0.5f;
                invaderExplosion.Play();
                Game1.score += scoreValue;
                Player.canShoot = true;
                Game1.invaders[ArrayPos.X,ArrayPos.Y] = null;
                currentIndex = 2;
                Destroy(other);
            }
        }

    }
}
