using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont arial;
        private static List<GameObject> objects = new List<GameObject>();
        public static GameObject obj_projectile, obj_electric, obj_missile;
        public static GameObject[,] invaders = new GameObject[11, 5];
        public static int[] invaderColumnLength = new int[11];
        public Texture2D invaderUFO, invaderTop, invaderMiddle,player ,invaderBottom, shield, shot1, shot2;
        public static int lives = 3;
        public static int score = 0000;
        public float move;
        int rightLimit = 0;
        int leftLimit = 0;
        int currentpos = 40;
        bool goingRight = true;
        bool goingDown = false;
        System.Random r = new System.Random();
        int invadersremaining = 0;
        public static float animationSpeed = 1.3f;
        public static SoundEffect shootSound;
        public static SoundEffect explosionSound;

        public static List<GameObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 224;
            graphics.PreferredBackBufferWidth = 256;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            base.Initialize();

            GameObject obj_invaderTop = new Invader(Vector2.Zero, 0, 1.3f, invaderTop, 3);
            GameObject obj_invaderMiddle = new Invader(Vector2.Zero, 0, 1.3f, invaderMiddle, 3);
            GameObject obj_invaderBottom = new Invader(Vector2.Zero, 0, 1.3f, invaderBottom, 3);
            GameObject obj_bigInvader = new BigInvader(Vector2.Zero, 40, 0, invaderUFO, 2);
            obj_projectile = new PlayerProjectile(Vector2.Zero, 200, 0, shot1, 1);
            obj_electric = new InvaderProjectile(Vector2.Zero, 100, 0, shot1, 1);
            obj_missile = new InvaderProjectile(Vector2.Zero, 100, 0, shot2, 1);

            for (int i = 0; i < invaders.GetLength(0); i++)
            { 
                for(int j =0; j < invaders.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        invaders[i, j] = obj_invaderTop.Clone();
                    }
                    if (j == 1 || j == 2)
                    {
                        invaders[i, j] = obj_invaderMiddle.Clone();
                    }
                    if (j == 3 || j == 4)
                    {
                        invaders[i, j] = obj_invaderBottom.Clone();
                    }
                    (invaders[i, j] as Invader).ArrayPos = new Point(i, j);
                    invaders[i, j].Position = new Vector2(40 + 16 * i, 15+ 16 * j);
                    objects.Add(invaders[i, j]);
                }
            }
            objects.Add(obj_bigInvader);
            objects.Add(new Shield(new Vector2(24 + 64 * 0, 150), 0, 0, shield, 12));
            objects.Add(new Shield(new Vector2(24 + 64 * 1, 150), 0, 0, shield, 12));
            objects.Add(new Shield(new Vector2(24 + 64 * 2, 150), 0, 0, shield, 12));
            objects.Add(new Shield(new Vector2(24 + 64 * 3, 150), 0, 0, shield, 12));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            invaderMiddle = Content.Load<Texture2D>("invader_Middle");
            invaderBottom = Content.Load<Texture2D>("invader_Bottom");
            invaderTop = Content.Load<Texture2D>("invader_Top");
            invaderUFO = Content.Load<Texture2D>("invader_UFO");
            shield = Content.Load<Texture2D>("shield");
            player = Content.Load<Texture2D>("player");
            Player.Instance.Sprite = Content.Load<Texture2D>("player");
            Player.Instance.LoadContent();
            shot1 = Content.Load<Texture2D>("shot_electric");
            shot2 = Content.Load<Texture2D>("shot_missile");
            arial = Content.Load<SpriteFont>("Font");
            shootSound = Content.Load<SoundEffect>("shoot");
            explosionSound = Content.Load<SoundEffect>("explosion");
            SoundEffect music = Content.Load<SoundEffect>("spaceinvaders1");
            SoundEffectInstance musicPlay = music.CreateInstance();
            musicPlay.IsLooped = false;
            musicPlay.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || lives<0)
            {
                Exit();
            }

            invadersremaining = 0;
            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    if (invaders[i, j] != null)
                    {
                        rightLimit = i;
                        invaderColumnLength[i] = j;
                        invadersremaining++;
                    }
                }
            }

            leftLimit = GetLeftLimit();
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (invadersremaining > 50)
            {
                animationSpeed = 1.3f;
            }
            if (invadersremaining < 50 && invadersremaining > 25)
            {
                animationSpeed = 5.3f;
            }
            if (invadersremaining < 25 && invadersremaining > 5)
            {
                animationSpeed = 10.3f;
            }
            if (invadersremaining < 5 && invadersremaining > 1)
            {
                animationSpeed = 40.3f;
            }
            if (invadersremaining == 1)
            {
                animationSpeed = 75.3f;
            }

            move += deltaTime * animationSpeed;
            
            if (move >= 1)
            {               
                shoot(r.Next(0,11));
                if (currentpos + rightLimit * 16 + 16 >= graphics.PreferredBackBufferWidth)
                {
                    goingRight = false;
                    goingDown = true;
                }

                if (currentpos + leftLimit*16 <= 0)
                {
                    goingRight = true;
                    goingDown = true;
                }

                if (goingRight)
                {
                    currentpos += 2;
                }
                else
                {
                    currentpos -= 2;
                }


                foreach (Invader inv in invaders)
                {
                    if (inv != null)
                    {
                        if (goingRight)
                        {
                            inv.Position += new Vector2(2, 0);
                        }
                        else
                        {
                            inv.Position += new Vector2(-2, 0);
                        }

                        if (goingDown)
                        {
                            inv.Position += new Vector2(0, 2);
                        }
                    }
                }
                goingDown = false;
                move =0;   
            }

            Player.Instance.Update(gameTime);

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void shoot(int column)
        {
            GameObject shot = obj_missile.Clone();
            switch(r.Next(0,2))
            {
                case 0:
                    shot = obj_electric.Clone();
                    break;

                case 1:
                    shot = obj_missile.Clone();
                    break;
            }
            if (invaderColumnLength[column] != 0)
            {
                shot.Position = new Vector2(currentpos + column * 16 + 8, invaderColumnLength[column] * 16 + 28);
                objects.Add(shot);
            }
        }

        private int GetLeftLimit()
        {

            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    if (invaders[i, j] != null)
                    {
                        return i;
                    }
                }
            }
            return 10;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            Player.Instance.Draw(spriteBatch);

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(spriteBatch);
            }

            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(player, new Vector2(10+16*i,200),new Rectangle(0,0,16,16), Color.White);
            }
            spriteBatch.DrawString(arial, "Score:", new Vector2(185, 190), Color.White);
            spriteBatch.DrawString(arial, ""+score, new Vector2(185, 205), Color.White);
            spriteBatch.DrawString(arial, "Lives:" + lives, new Vector2(10, 190), Color.White);

            if (invadersremaining == 0)
            {
                spriteBatch.DrawString(arial, "YOU WIN", new Vector2(256 / 2 - 32, 224 / 2 - 12), Color.White);
                spriteBatch.DrawString(arial, "Press escape to exit", new Vector2(256 / 2 - 72, 0), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
