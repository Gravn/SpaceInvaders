using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Space_Invaders
{
    class Player : GameObject
    {
        static Player instance;
        static Texture2D ssprite;
        public static bool canShoot = true;
        float timer1 = 0;
        float timer2 = 0;
        bool visible = true;
        Color playerColor = Color.White;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    
                    instance = new Player(new Vector2(256/2,180), 50,0, ssprite , 2);
                }
                return Player.instance;
            }
        }

        private Player(Vector2 position,float movementSpeed ,float animationSpeed,Texture2D sprite, int frames) : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            this.Sprite = sprite;
            ssprite = sprite;
            this.size = new Vector2(15, 8);
        }

        private void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                if (position.X > 0)
                direction += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                if(position.X < 240)
                direction += new Vector2(1, 0);
            }
            if (keyState.IsKeyDown(Keys.Space) && canShoot && currentIndex != 1)
            {
                SoundEffectInstance shootEffect = Game1.shootSound.CreateInstance();
                shootEffect.IsLooped = false;
                shootEffect.Volume = .5f;
                shootEffect.Play();

                //using Cloning/prototype
                GameObject myshot = Game1.obj_playerShot.Clone();
                myshot.Position = new Vector2(this.position.X + 6,this.position.Y - 6);
                Game1.Objects.Add(myshot);
                canShoot = false;   
            }
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            direction = Vector2.Zero;

            HandleInput(Keyboard.GetState());

            direction *= movementSpeed;


            position += (direction * deltaTime);

            if (currentIndex != 1)
            {
                
                base.Update(gameTime);
            }
            else
            {
                if (gameTime.TotalGameTime.TotalMilliseconds >= timer2)
                {
                    visible = !visible;
                    timer2 = (float)gameTime.TotalGameTime.TotalMilliseconds + 200;
                }
                if(visible)
                {
                    playerColor = Color.White;
                }
                else
                {
                    playerColor = Color.Tomato;
                }
                timer1 += deltaTime;
                if (timer1 >= 2)
                {
                    playerColor = Color.White;
                    currentIndex = 0;
                    timer1 = 0;
                }
            }
        }

        public override void OnCollision(GameObject other)
        {
                position = new Vector2(256 / 2, 180);
                Destroy(other);
                currentIndex = 1;
                Game1.lives--;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)position.X,(int)position.Y,16,16), rectangles[(int)currentIndex], playerColor);
            //base.Draw(spriteBatch);
        }
    }
}
