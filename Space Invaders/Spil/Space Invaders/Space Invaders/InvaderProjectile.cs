using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class InvaderProjectile : Projectile
    {
        public InvaderProjectile(Vector2 position, float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames) 
        {
            this.direction = new Vector2(0, 1);
        }
    }
}
