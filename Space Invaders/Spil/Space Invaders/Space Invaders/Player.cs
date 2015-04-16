using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    class Player : GameObject
    {
        static Player instance;
        static Texture2D ssprite;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    
                    instance = new Player(new Vector2(100,400), 100,0, ssprite , 2);
                }
                return Player.instance;
            }
        }

        private Player(Vector2 position,float movementSpeed ,float animationSpeed,Texture2D sprite, int frames) : base(position,movementSpeed,animationSpeed, sprite, frames)
        {
            this.Sprite = sprite;
            ssprite = sprite;
        }

        private void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                direction += new Vector2(0, -1);
            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                direction += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                direction += new Vector2(0, 1);
            }
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                direction += new Vector2(1, 0);
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                //Shoot

                //deathAnimTest
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            direction = Vector2.Zero;

            HandleInput(Keyboard.GetState());

            direction *= movementSpeed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += (direction * deltaTime);

            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            currentIndex = 1;
        }
    }
}
