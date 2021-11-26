using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IntroductieProject
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int id = 0;
        internal static Game GameInstance;
        BaseController gameState;

        /// <summary>
        /// The screen size for which the game is designed.
        /// All coordinates in the game are based on a screen of this size.
        /// </summary>
        internal static readonly Point ScreenSize = new Point(1600, 900);

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;



            GameInstance = this;
        }

        internal void startLevel()
        {
            gameState = new Level1Controller();
        }

        internal void startMultiPlayer()
        {
            gameState = new MultiPlayerController();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = new StartScreenController();


            _graphics.PreferredBackBufferWidth = ScreenSize.X;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = ScreenSize.Y;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }


        internal int getUniqueGameObjectID()
        {
            id++;
            return id;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.gameState.update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

       
            SpriteBatch sb = new SpriteBatch(this.GraphicsDevice);
            sb.Begin();
            gameState.draw(sb);
            sb.End();
           

            base.Draw(gameTime);
        }

        /// <summary>
        /// This is the function used for the first GIT excercise
        /// </summary>
        /// <returns></returns>
        internal string printStudentNames()
        {
            return "In elk geval niet Ivor";
        }

        internal Texture2D getSprite(string assetName)
        {
            Texture2D sprite;
            try
            {
                sprite = this.Content.Load<Texture2D>(assetName);
            }
            catch (ContentLoadException)
            {
                sprite = this.Content.Load<Texture2D>("missing_sprite");
            }

            return sprite;
        }
    }
}
