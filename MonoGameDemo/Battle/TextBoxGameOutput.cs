using System.Collections.Generic;
using System.Linq;
using BattleSystem.Abstractions.Control;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Actions.Buff;
using BattleSystem.Core.Actions.Damage;
using BattleSystem.Core.Actions.Heal;
using BattleSystem.Core.Actions.Protect;
using BattleSystem.Core.Actions.ProtectLimitChange;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Items;
using BattleSystem.Core.Moves;
using MonoGameDemo.Components;
using MonoGameDemo.Extensions;

namespace MonoGameDemo.Battle
{
    public class TextBoxGameOutput : IGameOutput
    {
        private readonly TextBoxComponent _textBox;

        public TextBoxGameOutput(TextBoxComponent textBox)
        {
            _textBox = textBox;
        }

        public void ShowBattleEnd(string winningTeam)
        {
            _textBox.AddText($"The {winningTeam} team wins!");
        }

        public void ShowCharacterSummary(Character character)
        {
            
        }

        public void ShowItemSummary(Item item)
        {
            _textBox.AddText($"{item.Name}: {item.Description}");
        }

        public void ShowMessage()
        {
            
        }

        public void ShowMessage(string message)
        {
            _textBox.AddText(message);
        }

        public void ShowMoveSetSummary(MoveSet moveSet)
        {
            
        }

        public void ShowMoveUse(MoveUse moveUse)
        {
            var text = $"{moveUse.User.Name} used {moveUse.Move.Name}!";
            foreach (var result in moveUse.ActionsResults)
            {
                text += " " + string.Join(" ", result.Results.Select(GetResultDescription));
            }

            _textBox.AddText(text);
        }

        public void ShowResult<TSource>(IActionResult<TSource> result)
        {
            _textBox.AddText(GetResultDescription(result));
        }

        private static string GetResultDescription<TSource>(IActionResult<TSource> result)
        {
            return result switch
            {
                BuffActionResult<TSource> bar => bar.Describe(),
                DamageActionResult<TSource> dar => dar.Describe(),
                HealActionResult<TSource> har => har.Describe(),
                ProtectActionResult<TSource> par => par.Describe(),
                ProtectLimitChangeActionResult<TSource> plcar => plcar.Describe(),
                _ => throw new System.NotImplementedException(),
            };
        }

        public void ShowStartTurn(int turnCounter)
        {
            _textBox.AddText($"Turn {turnCounter} start");
        }

        public void ShowTeamSummary(IEnumerable<Character> characters)
        {
            
        }
    }
}
