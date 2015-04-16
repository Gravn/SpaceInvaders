using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace DayTwoFrameworks
{
    class Player : SpriteObject
    {
        static Player instance;

        public static Player Instance
        {
            get 
            { 
                if (instance == null)
                {
                    instance = new Player(new Vector2(0,0), 12);
                }
                return Player.instance;
            }
        }

        private Player(Vector2 position, int frames) : base (position, frames)
        {
            //Creates the players animations.
            //CreateAnimation("IdleUp", 1, 50, 12, 50, 50, Vector2.Zero, 1);
            //CreateAnimation("RunUp", 12, 50, 0, 50, 50, Vector2.Zero, 12);
            //CreateAnimation("IdleRight", 1, 100, 8, 50, 50, Vector2.Zero, 1);
            //CreateAnimation("RunRight", 8, 100, 8, 50, 50, Vector2.Zero, 8);
            //CreateAnimation("IdleDown", 1, 0, 0, 50, 50, Vector2.Zero, 1);
            //CreateAnimation("RunDown", 12, 0, 0, 50, 50, Vector2.Zero, 12);
            //CreateAnimation("IdleLeft", 1, 100, 0, 50, 50, Vector2.Zero, 1);
            //CreateAnimation("RunLeft", 8, 100, 0, 50, 50, Vector2.Zero, 8);
            //CreateAnimation("AttackUp", 9, 230, 0, 70, 80, new Vector2(-13, -28), 27);
            //CreateAnimation("AttackRight", 9, 380, 0, 70, 70, new Vector2(10, -5), 27);
            //CreateAnimation("AttackDown", 9, 150, 0, 70, 80, new Vector2(-4, -2), 27);
            //CreateAnimation("Attackleft", 9, 310, 0, 70, 70, new Vector2(-30, -4), 27);
        }

        public override void LoadContent()
        {
            //texture = content.Load<Texture2D>(@"walk");

            base.LoadContent();
        }

        private void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                //Move up.
                //PlayAnimation("RunUp");
                velocity += new Vector2(0, -1);
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                //Attack Animation.
                //PlayAnimation("AttackUp");
            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                //Move left.
                //PlayAnimation("RunLeft");
                velocity += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                //Attack Animation.
                //PlayAnimation("AttackLeft");
            }
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                //Move down.
                //PlayAnimation("RunDown");
                velocity += new Vector2(0, 1);
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                //Attack Animation.
                //PlayAnimation("AttackDown");
            }
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                //Move right.
                //PlayAnimation("RunRight");
                velocity += new Vector2(1, 0);
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                //Attack Animation.
                //PlayAnimation("AttackRight");
            }
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            HandleInput(Keyboard.GetState());

            velocity *= speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += (velocity * deltaTime);

            base.Update(gameTime);
        }
    }
}
