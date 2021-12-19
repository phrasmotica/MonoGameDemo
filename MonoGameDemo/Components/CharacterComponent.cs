using System;
using BattleSystem.Core.Abilities;
using BattleSystem.Core.Actions.Damage;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameDemo.Battle;

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

            if (_isEnemy)
            {
                _position = new Vector2(600, 50);

                var moveSet = new MoveSet();

                moveSet.AddMove(
                    new MoveBuilder()
                        .Name("Scratch")
                        .Describe("Scratches the target.")
                        .WithMaxUses(40)
                        .AlwaysSucceeds()
                        .WithAction(
                            new DamageActionBuilder()
                                .AbsoluteDamage(10)
                                .TargetsFirstEnemy()
                                .Build()
                        )
                        .Build()
                );

                Character = new PatientCharacter("Wowowow", "b", 100, new StatSet
                {
                    Attack = new Stat(3),
                    Defence = new Stat(3),
                    Speed = new Stat(2),
                }, moveSet, new Ability(), new Random());
            }
            else
            {
                _position = new Vector2(50, 350);

                var moveSet = new MoveSet();

                moveSet.AddMove(
                    new MoveBuilder()
                        .Name("Scratch")
                        .Describe("Scratches the target.")
                        .WithMaxUses(40)
                        .AlwaysSucceeds()
                        .WithAction(
                            new DamageActionBuilder()
                                .AbsoluteDamage(10)
                                .TargetsFirstEnemy()
                                .Build()
                        )
                        .Build()
                );

                moveSet.AddMove(
                    new MoveBuilder()
                        .Name("Peck")
                        .Describe("Pecks the target.")
                        .WithMaxUses(30)
                        .AlwaysSucceeds()
                        .WithAction(
                            new DamageActionBuilder()
                                .AbsoluteDamage(20)
                                .TargetsFirstEnemy()
                                .Build()
                        )
                        .Build()
                );

                moveSet.AddMove(
                    new MoveBuilder()
                        .Name("Clonk")
                        .Describe("Clonks the target with a pointy elbow.")
                        .WithMaxUses(20)
                        .AlwaysSucceeds()
                        .WithAction(
                            new DamageActionBuilder()
                                .AbsoluteDamage(40)
                                .TargetsFirstEnemy()
                                .Build()
                        )
                        .Build()
                );

                moveSet.AddMove(
                    new MoveBuilder()
                        .Name("Throw")
                        .Describe("Throws the target in a big bear hug.")
                        .WithMaxUses(35)
                        .AlwaysSucceeds()
                        .WithAction(
                            new DamageActionBuilder()
                                .AbsoluteDamage(30)
                                .TargetsFirstEnemy()
                                .Build()
                        )
                        .Build()
                );

                Character = new PatientCharacter("Pick Pick Pick", "a", 100, new StatSet
                {
                    Attack = new Stat(3),
                    Defence = new Stat(3),
                    Speed = new Stat(3),
                }, moveSet, new Ability(), new Random());
            }
        }

        private ContentManager Content => _game.Content;
        private SpriteBatch SpriteBatch => _game.SpriteBatch;

        public PatientCharacter Character { get; }

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
            SpriteBatch.DrawString(_spriteFont, Character.Name, _position, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // draw health text
            var currentHealth = Math.Max(0, Character.CurrentHealth);
            var healthText = $"{currentHealth}HP/{Character.MaxHealth}HP";
            var nameTextOffsetY = _spriteFont.MeasureString(Character.Name).Y + 10;
            SpriteBatch.DrawString(_spriteFont, healthText, _position + new Vector2(0, nameTextOffsetY), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // draw sprite
            var healthTextOffsetY = _spriteFont.MeasureString(healthText).Y + 10;

            var effects = Character.IsDead ? SpriteEffects.FlipVertically : SpriteEffects.None;

            SpriteBatch.Draw(_texture, _position + new Vector2(0, nameTextOffsetY + healthTextOffsetY), null, Color.White, 0, Vector2.Zero, 0.7f, effects, 0);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
