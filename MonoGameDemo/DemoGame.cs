using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameDemo.Components;

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

        protected override void Initialize()
        {
            SetWindowSize();

            var font = Content.Load<SpriteFont>("font");

            var player = new CharacterComponent(this, font, false);
            Components.Add(player);

            var enemy = new CharacterComponent(this, font, true);
            Components.Add(enemy);

            var textBox = new TextBoxComponent(this, font);
            Components.Add(textBox);

            Components.Add(new AttacksComponent(this, player.Attacks, enemy, textBox));

            textBox.AddText($"What will {player.Name} do?");

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

        private void SetWindowSize()
        {
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.ApplyChanges();
        }
    }
}
