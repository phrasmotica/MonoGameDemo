using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameDemo.Components
{
    public class TextBoxComponent : DrawableGameComponent
    {
        private readonly DemoGame _game;
        private readonly SpriteFont _font;

        private readonly List<string> _textEntries;
        private readonly Vector2 _position;
        private readonly Texture2D _backgroundTexture;

        private KeyboardState lastKeyboard;
        private int entryIndex;

        public TextBoxComponent(DemoGame game, SpriteFont font) : base(game)
        {
            _game = game;
            _font = font;

            _textEntries = new List<string>();
            _position = new Vector2(550, 400);
            _backgroundTexture = CreateBackgroundTexture();
        }

        public int EntryIndex
        {
            get => entryIndex;
            set => entryIndex = MathHelper.Clamp(value, 0, _textEntries.Count - 1);
        }

        private string CurrentText => _textEntries.Any() ? _textEntries[EntryIndex] : null;

        public void AddText(string text)
        {
            _textEntries.Add(text);
            EntryIndex = _textEntries.Count - 1;
        }

        public override void Update(GameTime gameTime)
        {
            var currentKeyboard = Keyboard.GetState();

            if (currentKeyboard.IsKeyDown(Keys.Right) && lastKeyboard.IsKeyUp(Keys.Right))
            {
                EntryIndex++;
            }

            if (currentKeyboard.IsKeyDown(Keys.Left) && lastKeyboard.IsKeyUp(Keys.Left))
            {
                EntryIndex--;
            }

            lastKeyboard = currentKeyboard;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();

            _game.SpriteBatch.Draw(_backgroundTexture, _position, Color.White);

            var maxTextWidth = _backgroundTexture.Width - 10;
            var wrappedText = WrapText(CurrentText, maxTextWidth);
            _game.SpriteBatch.DrawString(_font, wrappedText, _position + new Vector2(5, 5), Color.Black);

            var indexText = $"{EntryIndex + 1}/{_textEntries.Count}";
            var indexTextSize = _font.MeasureString(indexText);
            var indexPosition = _position + new Vector2(5, _backgroundTexture.Height - 5 - indexTextSize.Y);
            _game.SpriteBatch.DrawString(_font, $"{EntryIndex + 1}/{_textEntries.Count}", indexPosition, Color.Black);

            _game.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private Texture2D CreateBackgroundTexture()
        {
            // create a texture where every pixel is white
            var texture = new Texture2D(_game.GraphicsDevice, 250, 200);

            var backgroundTextureData = new Color[250 * 200];

            for (var i = 0; i < backgroundTextureData.Length; i++)
            {
                backgroundTextureData[i] = Color.White;
            }

            texture.SetData(backgroundTextureData);

            return texture;
        }

        /// <summary>
        /// Returns a new line-wrapped version of the given text based on the max line width.
        /// Adapted from https://gist.github.com/Sankra/5585584.
        /// </summary>
        private string WrapText(string text, float maxLineWidth)
        {
            if (_font.MeasureString(text).X < maxLineWidth)
            {
                return text;
            }

            var words = text.Split(' ');
            var wrappedText = new StringBuilder();

            var lineWidth = 0f;
            var spaceWidth = _font.MeasureString(" ").X;

            for (int i = 0; i < words.Length; ++i)
            {
                var size = _font.MeasureString(words[i]);

                if (lineWidth + size.X < maxLineWidth)
                {
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    wrappedText.Append('\n');
                    lineWidth = size.X + spaceWidth;
                }

                wrappedText.Append(words[i]);
                wrappedText.Append(' ');
            }

            return wrappedText.ToString();
        }
    }
}
