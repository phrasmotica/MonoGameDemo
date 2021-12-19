using System.Linq;
using BattleSystem.Abstractions.Control;
using BattleSystem.Battles.TurnBased;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameDemo.Battle;
using MonoGameDemo.Components;

namespace MonoGameDemo
{
    public class DemoGame : Game
    {
        private CharacterComponent enemy;
        private PatientCharacter[] characters;
        private PatientTurnBasedBattle battle;
        private IGameOutput gameOutput;
        private AttacksComponent attacksComponent;

        public DemoGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public GraphicsDeviceManager Graphics { get; }
        public SpriteBatch SpriteBatch { get; private set; }

        protected override void Initialize()
        {
            SetWindowSize();

            var font = Content.Load<SpriteFont>("font");

            var player = new CharacterComponent(this, font, false);
            Components.Add(player);

            enemy = new CharacterComponent(this, font, true);
            Components.Add(enemy);

            var textBox = new TextBoxComponent(this, font);
            Components.Add(textBox);

            var moveProcessor = new MoveProcessor();
            gameOutput = new TextBoxGameOutput(textBox);

            characters = new[] { player.Character, enemy.Character };

            battle = new PatientTurnBasedBattle(
                moveProcessor,
                new ActionHistory(),
                gameOutput,
                characters
            );

            attacksComponent = new AttacksComponent(this, player);
            Components.Add(attacksComponent);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            while (battle.CurrentPhase < BattlePhase.TurnChoice)
            {
                battle.Next();
            }

            if (!enemy.Character.HasChosenMove)
            {
                enemy.Character.SetRandomMoveChoice();
            }

            if (characters.All(c => c.HasChosenMove))
            {
                battle.Next(); // now at turn execute
                battle.Next(); // now at turn end
                battle.Next(); // now at turn start or battle end
            }

            if (battle.CurrentPhase == BattlePhase.BattleEnd)
            {
                attacksComponent.Disable();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        private void SetWindowSize()
        {
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.ApplyChanges();
        }
    }
}
