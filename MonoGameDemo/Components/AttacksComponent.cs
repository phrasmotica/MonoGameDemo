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

        private Vector2 _position;

        public AttacksComponent(DemoGame game, List<Attack> attacks, CharacterComponent target) : base(game)
        {
            _game = game;
            _attacks = attacks;
            _target = target;

            _position = new Vector2(400, 400);
        }

        public override void Initialize()
        {
            for (int i = 0; i < _attacks.Count; i++)
            {
                var attack = _attacks[i];

                _game.Components.Add(new ButtonComponent(_game)
                {
                    TextFunc = () => $"{attack.Name} ({attack.Power}HP)",
                    Position = _position + new Vector2(0, i * 30),
                    OnClick = () =>
                    {
                        Debug.WriteLine($"{attack.Name} was used on {_target.Name}!");
                        _target.Health.Damage(attack.Power);
                    },
                });
            }

            base.Initialize();
        }
    }
}
