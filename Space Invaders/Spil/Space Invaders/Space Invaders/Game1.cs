using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        public static Texture2D invaderUFO, invaderTop, invaderMiddle, invaderBottom, player, shield, shot1, shot2, explosion;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
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

            //Invader setup test
            objects.Add(new Invader(new Vector2(400, 100 + 16 * 0), 60, 1.3f, invaderUFO, 1));

            objects.Add(new Invader(new Vector2(400, 100 + 16 * 1), 60, 1.3f, invaderTop, 2));
            objects.Add(new Invader(new Vector2(400, 100 + 16 * 2), 60, 1.3f, invaderMiddle, 2));
            objects.Add(new Invader(new Vector2(400, 100 + 16 * 3), 60, 1.3f, invaderMiddle, 2));
            objects.Add(new Invader(new Vector2(400, 100 + 16 * 4), 60, 1.3f, invaderBottom, 2));
            objects.Add(new Invader(new Vector2(400, 100 + 16 * 5), 60, 1.3f, invaderBottom, 2));
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
            Player.Instance.Sprite = Content.Load<Texture2D>("player");
            Player.Instance.LoadContent();

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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Player.Instance.Update(gameTime);

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update(gameTime);
            }
                base.Update(gameTime);
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
