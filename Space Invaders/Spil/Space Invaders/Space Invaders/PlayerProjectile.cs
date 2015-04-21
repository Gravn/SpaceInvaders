using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class PlayerProjectile : Projectile
    {
        public PlayerProjectile(Vector2 position,float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames) 
        {
            this.direction = new Vector2(0, -1);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is InvaderProjectile)
            {
                Player.canShoot = true;
                Destroy(other);
                Destroy(this);
            }
        }

        public override void Destroy(GameObject obj)
        {
            Player.canShoot = true;
            base.Destroy(obj);
        } 
    }
}
