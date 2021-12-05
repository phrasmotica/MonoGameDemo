using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameDemo.Data;

namespace MonoGameDemo.Components
{
    public class CharacterComponent : DrawableGameComponent
    {
        private readonly DemoGame _game;
        private readonly SpriteFont _spriteFont;
        private readonly bool _isEnemy;

        private Vector2 _position;
        private Texture2D _texture;

        public CharacterComponent(DemoGame game, SpriteFont spriteFont, bool isEnemy) : base(game)
        {
            _game = game;
            _spriteFont = spriteFont;
            _isEnemy = isEnemy;

            Health = new Health(100);
            Attacks = new List<Attack>();

            if (_isEnemy)
            {
                _position = new Vector2(600, 50);

                Name = "Wowowow";
            }
            else
            {
                _position = new Vector2(50, 350);

                Name = "Pick Pick Pick";

                Attacks.Add(new Attack("Scratch", 10));
                Attacks.Add(new Attack("Peck", 20));
                Attacks.Add(new Attack("Clonk", 40));
                Attacks.Add(new Attack("Throw", 30));
            }
        }

        private ContentManager Content => _game.Content;
        private SpriteBatch SpriteBatch => _game.SpriteBatch;

        public string Name { get; set; }
        public Health Health { get; set; }
        public List<Attack> Attacks { get; }

        public bool IsFainted => Health.Current <= 0;

        protected override void LoadContent()
        {
            var textureName = _isEnemy ? "eevee" : "piplup";
            _texture = Content.Load<Texture2D>(textureName);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            // draw name text
            SpriteBatch.DrawString(_spriteFont, Name, _position, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // draw health text
            var healthText = $"{Health.Current}HP/{Health.Max}HP";
            var nameTextOffsetY = _spriteFont.MeasureString(Name).Y + 10;
            SpriteBatch.DrawString(_spriteFont, healthText, _position + new Vector2(0, nameTextOffsetY), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // draw sprite
            var healthTextOffsetY = _spriteFont.MeasureString(healthText).Y + 10;

            var effects = IsFainted ? SpriteEffects.FlipVertically : SpriteEffects.None;

            SpriteBatch.Draw(_texture, _position + new Vector2(0, nameTextOffsetY + healthTextOffsetY), null, Color.White, 0, Vector2.Zero, 0.7f, effects, 0);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
