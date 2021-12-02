using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameDemo
{
    public class Button : DrawableGameComponent
    {
        private readonly DemoGame _game;

        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;

        public Button(DemoGame game, Texture2D texture, SpriteFont font) : base(game)
        {
            _game = game;
            _texture = texture;
            _font = font;
            PenColour = Color.Black;
        }

        private SpriteBatch SpriteBatch => _game.SpriteBatch;

        public Action OnClick;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle => new((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height);

        public Func<string> TextFunc { get; set; }
        public string Text => TextFunc();

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    OnClick();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var colour = Color.White;

            if (_isHovering)
            {
                colour = Color.Gray;
            }

            SpriteBatch.Begin();

            SpriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(Text).Y / 2);

                SpriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }

            SpriteBatch.End();
        }
    }
}
