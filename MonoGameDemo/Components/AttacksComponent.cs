using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameDemo.Components
{
    public class AttacksComponent : DrawableGameComponent
    {
        private readonly DemoGame _game;
        private readonly CharacterComponent _user;

        private readonly Vector2 _position;
        private readonly List<ButtonComponent> _buttons;

        public AttacksComponent(
            DemoGame game,
            CharacterComponent user) : base(game)
        {
            _game = game;
            _user = user;

            _position = new Vector2(400, 400);
            _buttons = new List<ButtonComponent>();
        }

        public override void Initialize()
        {
            var user = _user.Character;
            var moves = user.Moves.Moves;

            for (int i = 0; i < moves.Count; i++)
            {
                var moveIndex = i; // required to have its captured correctly by the OnClick lambda
                var attack = moves[moveIndex];

                var button = new ButtonComponent(_game)
                {
                    TextFunc = () => $"{attack.Name} ({attack.RemainingUses}/{attack.MaxUses})",
                    Position = _position + new Vector2(0, i * 30),
                    OnClick = () => user.ChosenMoveIndex = moveIndex,
                };

                _buttons.Add(button);
                _game.Components.Add(button);
            }

            base.Initialize();
        }

        public void Disable() => _buttons.ForEach(b => b.Disabled = true);
    }
}
