using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoGameDemo.Data;

namespace MonoGameDemo.Components
{
    public class AttacksComponent : DrawableGameComponent
    {
        private readonly DemoGame _game;
        private readonly List<Attack> _attacks;
        private readonly CharacterComponent _target;
        private readonly TextBoxComponent _textBox;

        private readonly Vector2 _position;
        private readonly List<ButtonComponent> _buttons;

        public AttacksComponent(DemoGame game, List<Attack> attacks, CharacterComponent target, TextBoxComponent textBox) : base(game)
        {
            _game = game;
            _attacks = attacks;
            _target = target;
            _textBox = textBox;

            _position = new Vector2(400, 400);
            _buttons = new List<ButtonComponent>();
        }

        public override void Initialize()
        {
            for (int i = 0; i < _attacks.Count; i++)
            {
                var attack = _attacks[i];

                var button = new ButtonComponent(_game)
                {
                    TextFunc = () => $"{attack.Name} ({attack.Power}HP)",
                    Position = _position + new Vector2(0, i * 30),
                    OnClick = () =>
                    {
                        if (_target.IsFainted)
                        {
                            return;
                        }

                        var amount = _target.Health.Damage(attack.Power);
                        var text = $"{attack.Name} was used on {_target.Name} and did {amount} damage!";

                        if (_target.IsFainted)
                        {
                            text += $" {_target.Name} fainted!";
                            Disable();
                        }

                        _textBox.AddText(text);
                    },
                };

                _buttons.Add(button);
                _game.Components.Add(button);
            }

            base.Initialize();
        }

        public void Disable() => _buttons.ForEach(b => b.Disabled = true);
    }
}
