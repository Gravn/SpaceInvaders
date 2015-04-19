using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    class Shield : GameObject
    {
        public Rectangle[,] rectangles2d;
        public int[] currentIndexes = new int[12];
        public Rectangle[] collisionBoxes = new Rectangle[12];

        public Shield(Vector2 position, float movementSpeed, float animationSpeed, Texture2D sprite, int frames)
            : base(position, movementSpeed, animationSpeed, sprite, frames)
        {
            LoadContent();
            collisionBoxes[0] = new Rectangle((int)this.position.X,(int)this.position.Y, 7, 5);
            collisionBoxes[1] = new Rectangle((int)this.position.X+7, (int)this.position.Y, 8, 5);
            collisionBoxes[2] = new Rectangle((int)this.position.X+7+8, (int)this.position.Y, 7, 5);

            collisionBoxes[3] = new Rectangle((int)this.position.X, (int)this.position.Y+5, 7, 4);
            collisionBoxes[4] = new Rectangle((int)this.position.X+7, (int)this.position.Y+5, 8, 4);
            collisionBoxes[5] = new Rectangle((int)this.position.X+8, (int)this.position.Y+5, 7, 4);

            collisionBoxes[6] = new Rectangle((int)this.position.X, (int)this.position.Y+5+4, 7, 3);
            collisionBoxes[7] = new Rectangle((int)this.position.X+7, (int)this.position.Y+5+4, 8, 3);
            collisionBoxes[8] = new Rectangle((int)this.position.X+8, (int)this.position.Y+5+4, 7, 3);

            collisionBoxes[9] = new Rectangle((int)this.position.X, (int)this.position.Y+5+4+3, 7, 4);

            collisionBoxes[11] = new Rectangle((int)this.position.X+8, (int)this.position.Y+5+4+3, 7, 4);
        }

        public override void Update(GameTime gameTime)
        {
            
            CheckCollision();
        }

        public override void CheckCollision()
        {
            for (int i = 0; i < Game1.Objects.Count; i++)
            {
                if (Game1.Objects[i] != this)
                {
                    for (int j = 0; j < collisionBoxes.Length; j++)
                    {
                        if (this.collisionBoxes[j].Intersects(Game1.Objects[i].collisionBox))
                        {
                            if (this.currentIndexes[j] < 5)
                            {
                                currentIndexes[j] = 1;
                                OnCollision(Game1.Objects[i]);
                            }
                        }
                    }
                }
            }
        }

        public override void OnCollision(GameObject other)
        {

            base.OnCollision(other);
        }

        public override void LoadContent()
        {

            int width = sprite.Width / frames;
            int height = sprite.Height / frames;
            rectangles2d = new Rectangle[frames,frames];

            for (int i = 0; i < frames; i++)
            {
                for (int j = 0; j < frames; j++)
                {
                    rectangles2d[i,j] = new Rectangle(i * width, j * height, width, height);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(position.X, position.Y), rectangles2d[(int)currentIndexes[0], 0], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7, position.Y), rectangles2d[(int)currentIndexes[1], 1], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7 + 8, position.Y), rectangles2d[(int)currentIndexes[2], 2], Color.White);

            spriteBatch.Draw(sprite, new Vector2(position.X, position.Y + 4), rectangles2d[(int)currentIndexes[3], 3], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7, position.Y + 4), rectangles2d[(int)currentIndexes[4], 4], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7 + 8, position.Y + 4), rectangles2d[(int)currentIndexes[5], 5], Color.White);

            spriteBatch.Draw(sprite, new Vector2(position.X, position.Y + 7), rectangles2d[(int)currentIndexes[6], 6], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7, position.Y + 7), rectangles2d[(int)currentIndexes[7], 7], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7 + 8, position.Y + 7), rectangles2d[(int)currentIndexes[8], 8], Color.White);

            spriteBatch.Draw(sprite, new Vector2(position.X, position.Y + 11), rectangles2d[(int)currentIndexes[9], 9], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7, position.Y + 11), rectangles2d[(int)currentIndexes[10], 10], Color.White);
            spriteBatch.Draw(sprite, new Vector2(position.X + 7 + 8, position.Y + 11), rectangles2d[(int)currentIndexes[11], 11], Color.White);
            
            //base.Draw(spriteBatch);
        } 
    }
}
