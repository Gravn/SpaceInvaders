using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


//Debug
using System.Diagnostics;

namespace Space_Invaders
{


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private static List<GameObject> objects = new List<GameObject>();
        
        public static List<GameObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        public static GameObject obj_projectile;

        public static GameObject[,] invaders = new GameObject[11, 5];
        public static int[] invaderColumnLength = new int[11];
        public static Texture2D invaderUFO, invaderTop, invaderMiddle, invaderBottom, player, shield, shot1, shot2, explosion;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int lives = 3;
        public float move;
        int rightLimit = 0;
        int leftLimit = 0;
        int currentpos = 0;
        bool goingRight = true;
        bool goingDown = false;

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
            GameObject obj_bigInvader = new BigInvader(Vector2.Zero, 80, 0, invaderUFO, 2);
            obj_projectile = new Projectile(Vector2.Zero, 100, 0, shot1, 1);

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
                    invaders[i, j].Position = new Vector2(0 + 16 * i, 15 + 16 * j);
                    objects.Add(invaders[i, j]);
                }
            }
            objects.Add(obj_bigInvader);

            objects.Add(new Shield(new Vector2(24 + 64 * 0, 180), 0, 0, shield, 12));
            objects.Add(new Shield(new Vector2(24 + 64 * 1, 180), 0, 0, shield, 12));
            objects.Add(new Shield(new Vector2(24 + 64 * 2, 180), 0, 0, shield, 12));
            objects.Add(new Shield(new Vector2(24 + 64 * 3, 180), 0, 0, shield, 12));


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
            Player.Instance.Sprite = Content.Load<Texture2D>("player");
            Player.Instance.LoadContent();
            shot1 = Content.Load<Texture2D>("shot_electric");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    if (invaders[i, j] != null)
                    {
                        rightLimit = i;
                        invaderColumnLength[i] = j;
                    }
                }
            }

            leftLimit = GetLeftLimit();

            //Debug.WriteLine("Invader[11,0]: " + invaders[10, 0].ToString());
            
            Debug.WriteLine("ColumnLength[0]:" + invaderColumnLength[0]);
            //Debug.WriteLine("LeftLimit:" +  leftLimit);  

            System.Random r = new System.Random();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            move += deltaTime * 1.3f;
            if (move >= 1f)
            {
                shoot(r.Next(0,11));
                if (currentpos + rightLimit * 16 + 16 >= graphics.PreferredBackBufferWidth)
                {
                    goingRight = false;

                }

                if (currentpos + leftLimit*16 <= 0)
                {
                    goingRight = true;
                }

                if (goingRight)
                {
                    currentpos++;
                }
                else
                {
                    currentpos--;
                }


                foreach (Invader inv in invaders)
                {
                    if (inv != null)
                    {
                        if (goingRight)
                        {
                            inv.Position += new Vector2(1, 0);
                        }
                        else
                        {
                            inv.Position += new Vector2(-1, 0);
                        }
                    }
                }
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
            GameObject shot = obj_projectile.Clone();
            shot.Position = new Vector2(currentpos + column * 16 + 8, invaderColumnLength[column] * 16+28);
            objects.Add(shot);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
