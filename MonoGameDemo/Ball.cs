using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameDemo
{
    public class Ball : DrawableGameComponent
    {
        private readonly DemoGame _game;

        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;

        public Ball(DemoGame game) : base(game)
        {
            _game = game;
        }

        private GraphicsDeviceManager Graphics => _game.Graphics;
        private ContentManager Content => _game.Content;
        private SpriteBatch SpriteBatch => _game.SpriteBatch;

        public override void Initialize()
        {
            _position = new Vector2(Graphics.PreferredBackBufferWidth / 2, Graphics.PreferredBackBufferHeight / 2);
            _speed = 100;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _texture = Content.Load<Texture2D>("ball");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _position.Y -= _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _position.Y += _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _position.X -= _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _position.X += _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_position.X > Graphics.PreferredBackBufferWidth - _texture.Width / 2)
            {
                _position.X = Graphics.PreferredBackBufferWidth - _texture.Width / 2;
            }
            else if (_position.X < _texture.Width / 2)
            {
                _position.X = _texture.Width / 2;
            }

            if (_position.Y > Graphics.PreferredBackBufferHeight - _texture.Height / 2)
            {
                _position.Y = Graphics.PreferredBackBufferHeight - _texture.Height / 2;
            }
            else if (_position.Y < _texture.Height / 2)
            {
                _position.Y = _texture.Height / 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(_texture, _position, null, Color.White, 0, new Vector2(_texture.Width / 2, _texture.Height / 2), Vector2.One, SpriteEffects.None, 0);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
