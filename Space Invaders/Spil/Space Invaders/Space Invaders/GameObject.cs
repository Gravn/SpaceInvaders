using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    public abstract class GameObject
    {
        protected Vector2 position;
        protected Vector2 direction;
        protected float animationSpeed;
        protected float movementSpeed;
        protected Texture2D sprite;
        //private Texture2D collisionBoxTexture; no need update diagram
        protected Rectangle[] rectangles;
        protected int frames;
        //private int currentIndex;
        //private float timeElapsed;
        //protected float fps = 60;
        //Test
        protected float currentIndex;
        //Test End

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Rectangle collisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)Sprite.Width / 2, (int)Sprite.Height / 2); }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public GameObject(Vector2 position,float movementSpeed, float animationSpeed,Texture2D sprite, int frames)
        {
            this.position = position;
            this.animationSpeed = animationSpeed;
            this.movementSpeed = movementSpeed;
            this.frames = frames;
            this.sprite = sprite;
        }

        public virtual void LoadContent()
        {
            int width = sprite.Width / frames;

            rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle(i * width, 0, width, sprite.Height);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            //old
            //timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //currentIndex = (int)(timeElapsed * fps);

            //if (currentIndex > rectangles.Length - 1)
            //{
            //    timeElapsed = 0;
            //    currentIndex = 0;
            //}


            UpdateAnimation(gameTime);
            CheckCollision();

        }

        public virtual void UpdateAnimation(GameTime gameTime)
        {
            //Fps dependent animation
            //Start
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentIndex += deltaTime * animationSpeed;
            if (collisionBox.Right > 10)
            {

            }
            if (currentIndex >= rectangles.Length - 1)
            {
                currentIndex = 0;
            }
            //End
        }

        public virtual void CheckCollision()
        {
            for (int i = 0; i < Game1.Objects.Count; i++)
            {
                if (Game1.Objects[i] != this)
                {
                    if (this.collisionBox.Intersects(Game1.Objects[i].collisionBox))
                    {
                        OnCollision(Game1.Objects[i]);
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, rectangles[(int)currentIndex], Color.White);
        }

        public virtual void OnCollision(GameObject other) { }

        public virtual void Destroy(GameObject obj)
        {
            Game1.Objects.Remove(obj);
        }

        public GameObject Clone()
        {
            GameObject clone = (GameObject)this.MemberwiseClone();
            return clone;
        }
    }
}
