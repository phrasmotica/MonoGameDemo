using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameDemo.Components
{
    /// <summary>
    /// Component for rendering a clickable button. Adapted from
    /// https://github.com/Oyyou/MonoGame_Tutorials/blob/master/MonoGame_Tutorials/Tutorial012/Controls/Button.cs.
    /// </summary>
    public class ButtonComponent : DrawableGameComponent
    {
        private readonly DemoGame _game;
        private readonly SpriteFont _font;
        private readonly Texture2D _texture;

        private MouseState _lastMouse;
        private bool _isHovering;

        public ButtonComponent(DemoGame game, Texture2D texture = null, SpriteFont font = null) : base(game)
        {
            _game = game;
            _texture = texture ?? _game.Content.Load<Texture2D>("button");
            _font = font ?? _game.Content.Load<SpriteFont>("font");
            PenColour = Color.Black;
        }

        private SpriteBatch SpriteBatch => _game.SpriteBatch;

        public Action OnClick { get; set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle => new((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height);

        public Func<string> TextFunc { get; set; }
        public string Text => TextFunc();

        public bool Disabled { get; set; }

        public override void Update(GameTime gameTime)
        {
            var currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            _isHovering = false;

            if (!Disabled && mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && _lastMouse.LeftButton == ButtonState.Pressed)
                {
                    OnClick();
                }
            }

            _lastMouse = currentMouse;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var colour = Color.White;

            if (Disabled)
            {
                colour = Color.LightGray;
                PenColour = Color.Gray;
            }
            else if (_isHovering)
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
