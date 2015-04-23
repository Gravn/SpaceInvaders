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
        protected Vector2 size;
        protected Vector2 direction;
        protected float animationSpeed;
        protected float movementSpeed;
        protected Texture2D sprite;
        protected Rectangle[] rectangles;
        protected int frames;
        protected float currentIndex;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Rectangle collisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y,(int)this.size.X, (int)this.size.Y); }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        public GameObject(Vector2 position, float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
        {
            this.position = position;
            this.animationSpeed = animationSpeed;
            this.movementSpeed = movementSpeed;
            this.frames = frames;
            this.sprite = sprite;
            this.size = new Vector2(16,8);
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
            UpdateAnimation(gameTime);
            CheckCollision();
        }

        public virtual void UpdateAnimation(GameTime gameTime)
        {
            //Fps dependent animation
            //Start
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentIndex += deltaTime * Game1.animationSpeed;
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
