using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameDemo
{
    public class DemoGame : Game
    {
        public DemoGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public GraphicsDeviceManager Graphics { get; }
        public SpriteBatch SpriteBatch { get; private set; }

        public int BallCount => Components.OfType<Ball>().Count();

        protected override void Initialize()
        {
            Components.Add(new Ball(this));

            var button = new Button(this, Content.Load<Texture2D>("button"), Content.Load<SpriteFont>("font"))
            {
                Position = new Vector2(10, 10),
                TextFunc = () => $"Create Ball ({BallCount})",
                OnClick = () => Components.Add(new Ball(this)),
            };

            Components.Add(button);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
