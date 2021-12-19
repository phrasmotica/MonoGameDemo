using System;
using System.Collections.Generic;
using BattleSystem.Core.Abilities;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Stats;

namespace MonoGameDemo.Battle
{
    public class PatientCharacter : Character
    {
        private readonly Random _random;

        public PatientCharacter(
            string name,
            string team,
            int maxHealth,
            StatSet stats,
            MoveSet moves,
            Ability ability,
            Random random) : base(name, team, maxHealth, stats, moves, ability)
        {
            _random = random;
        }

        public int? ChosenMoveIndex { get; set; }

        public bool HasChosenMove => ChosenMoveIndex.HasValue;

        public override MoveUse ChooseMove(IEnumerable<Character> otherCharacters)
        {
            if (!HasChosenMove)
            {
                throw new InvalidOperationException($"Move index must be set for {Name}!");
            }

            var chosenMoveIndex = ChosenMoveIndex.Value;
            ChosenMoveIndex = null;

            return new MoveUse
            {
                Move = Moves.GetMove(chosenMoveIndex),
                User = this,
                OtherCharacters = otherCharacters,
            };
        }

        public void SetRandomMoveChoice()
        {
            ChosenMoveIndex = _random.Next(Moves.Moves.Count);
        }
    }
}
