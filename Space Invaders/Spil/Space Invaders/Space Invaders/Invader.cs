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
        private Point arrayPos = Point.Zero;

        public Point ArrayPos
        {
            get { return arrayPos; }
            set { arrayPos = value;}
        }

        public Invader(Vector2 position,float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            LoadContent();
        }

        public override void Destroy(GameObject obj)
        {           
            base.Destroy(obj);
        }

    }
}
