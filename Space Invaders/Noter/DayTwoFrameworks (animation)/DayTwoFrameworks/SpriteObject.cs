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
    abstract class SpriteObject
    {
        protected Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        private Rectangle[] rectangles;
        protected Vector2 position = Vector2.Zero;
        private Vector2 origin = Vector2.Zero;
        private float layer = 0;
        private float scale = 1;
        private Color color = Color.White;
        private SpriteEffects effect = new SpriteEffects();
        protected Vector2 velocity = Vector2.Zero;
        protected float speed = 100;
        protected int frames;
        private int currentIndex;
        private float timeElapsed;
        protected float fps = 10;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public SpriteObject(Vector2 position, int frames)
        {
            this.position = position;
            this.frames = frames;
           
        }

        public virtual void LoadContent()
        {
            int width = texture.Width / frames;

            rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle(i * width, 0, width, texture.Height);
            }
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, rectangles[currentIndex], color, 0, origin, scale, effect, layer);
        }

        public virtual void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                timeElapsed = 0;
                currentIndex = 0;
            }
        }

        protected void CreateAnimation(string name, int frames, int yPos, int xStartFrame, int width, int height, Vector2 offset, float fps)
        {
            animations.Add(name, new Animation(frames, yPos, xStartFrame, width, height, offset, fps));
        }

        public void PlayAnimation(string name)
        {
        }
    }
}
