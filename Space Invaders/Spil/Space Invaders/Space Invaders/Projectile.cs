﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Projectile : GameObject
    {
        public Vector2 direction = new Vector2(0,1);

        public Projectile (Vector2 position,float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            size = new Vector2(3, 6);
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += new Vector2(0, movementSpeed*deltaTime*direction.Y);

            if (position.Y < 0)
            {
                Destroy(this);
            }
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Shield)
            {
                //code here
            }

        }
    }
}
