﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class BigInvader : Invader
    {
        public BigInvader(Vector2 position, float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }     
}