using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FunctionalityLib;
using FunctionalityLib.TileEngine;
using FunctionalityLib.SpriteClasses;
using FunctionalityLib.WorldClasses;
using VideoGameSemester.Components;
using FunctionalityLib.Controls;
using Microsoft.Xna.Framework.Content;
using FunctionalityLib.CharacterClasses;

namespace VideoGameSemester.GameScreens
{
    public class CombatScreen : BaseGameState
    {
        #region Field Region

        //GUI stuff
        Texture2D backgroundImageBackground;
        Texture2D backgroundImageForeground;
        Texture2D monsterImage;
        Texture2D textBox;
        Rectangle statusBoxRectangle;
        Rectangle combatMessageRectangle;
        LinkLabel attackLabel;
        LinkLabel winLabel;
        Label playerHP;
        Label enemyHP;
        Label combatMessage;
        Song song;

        Queue<string> combatLog;

        Entity monsterFromData;
        Monster monster;

        float timetoDelayCombat = 4.0f;

        bool playerSelectedAction = false;

        public static bool battleBegin = false;

        //Sprite Position stuff
        static Vector2 gamePlayScreenPosition;
        static AnimationKey gamePlayScreenAnimation;

        #endregion

        #region Property Region
        #endregion

        #region Constructor Region

        public CombatScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #endregion

        #region XNA Method Region

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            song = Content.Load<Song>(@"Music\Battle3");

            base.LoadContent();
            combatLog = new Queue<string>();
            Begin();

            backgroundImageBackground = Content.Load<Texture2D>(@"Backgrounds\Battlebacks Background\Grassland");
            backgroundImageForeground = Content.Load<Texture2D>(@"Backgrounds\Battlebacks Foreground\Meadow");
            textBox = Content.Load<Texture2D>(@"GUI\textbox");
            statusBoxRectangle = new Rectangle(0, 650, GameRef.ScreenRectangle.Width, GameRef.ScreenRectangle.Height - 650);
            combatMessageRectangle = new Rectangle((GameRef.ScreenRectangle.Width) - (GameRef.ScreenRectangle.Width), 0, GameRef.ScreenRectangle.Width, 80);

            playerHP = new Label();
            playerHP.Position = new Vector2(950, 670);
            playerHP.Text = GamePlayScreen.Player.Character.Entity.Health.CurrentValue + "";
            playerHP.Color = Color.White;
            playerHP.TabStop = false;
            playerHP.HasFocus = false;

            enemyHP = new Label();
            enemyHP.Position = new Vector2(100, 670);
            enemyHP.Text = monster.Entity.Health.CurrentValue + "";
            enemyHP.Color = Color.White;
            enemyHP.TabStop = false;
            enemyHP.HasFocus = false;

            combatMessage = new Label();
            combatMessage.Text = "";
            combatMessage.Position = new Vector2((GameRef.ScreenRectangle.Width / 2) - 230 , 0);     

            combatMessage.Color = Color.White;
            combatMessage.TabStop = false;
            combatMessage.HasFocus = false;

            attackLabel = new LinkLabel();
            attackLabel.Position = new Vector2(920, 650);
            attackLabel.Text = "Attack";
            attackLabel.Color = Color.White;
            attackLabel.TabStop = true;
            attackLabel.HasFocus = true;
            attackLabel.Selected += new EventHandler(attackLabel_Selected);

            winLabel = new LinkLabel();
            winLabel.Position = new Vector2(GameRef.ScreenRectangle.Width/2, GameRef.ScreenRectangle.Height/2);
            winLabel.Text = "You Win!";
            winLabel.Color = Color.White;
            winLabel.TabStop = true;
            winLabel.HasFocus = true;
            winLabel.Visible = false;
            winLabel.Selected += new EventHandler(winLabel_Selected);     

            ControlManager.Add(attackLabel);
            ControlManager.Add(playerHP);
            ControlManager.Add(winLabel);
            ControlManager.Add(combatMessage);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            string combatLog = string.Empty;

            if (battleBegin == true)
            {
                Begin();
            }

            if (monster.Entity.Health.GetCurrent() <= 0)
            {
                winLabel.Visible = true;
                winLabel.HasFocus = true;
                winLabel.TabStop = true;
                attackLabel.TabStop = false;
                ControlManager.NextControl();
            }
            else
            {
                attackLabel.TabStop = true;
                winLabel.Visible = false;
                winLabel.HasFocus = false;
                winLabel.TabStop = false;
            }

            if(playerSelectedAction == true)
            {
                playerSelectedAction = false;
                preformAttack(gameTime);
                combatMessage.Text = getCombatLogText();
            }

            playerHP.Text = GamePlayScreen.Player.Character.Entity.Health.CurrentValue + "";
            enemyHP.Text = monster.Entity.Health.CurrentValue + "";

            ControlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(
                backgroundImageForeground,
                GameRef.ScreenRectangle,
                Color.White);

            GameRef.SpriteBatch.Draw(
                backgroundImageBackground,
                GameRef.ScreenRectangle,
                Color.White);

            GameRef.SpriteBatch.Draw(
                textBox,
                statusBoxRectangle,
                Color.White * 0.65f);

            GameRef.SpriteBatch.Draw(
                textBox,
                combatMessageRectangle,
                Color.White * 0.65f);

            ControlManager.Draw(GameRef.SpriteBatch);
            GamePlayScreen.Player.Sprite.Draw(gameTime, GameRef.SpriteBatch);
            monster.Sprite.Draw(gameTime, GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Combat Screen Methods

        private void attackLabel_Selected(object sender, EventArgs e)
        {
            playerSelectedAction = true;
        }

        private void winLabel_Selected(object sender, EventArgs e)
        {
            GamePlayScreen.Player.Sprite.Position = gamePlayScreenPosition;
            GamePlayScreen.Player.Sprite.CurrentAnimation = gamePlayScreenAnimation;
            GamePlayScreen.Player.Sprite.scale = 1.0f;
            monster.Sprite.scale = 1.0f;
            UnloadContent();
            Transition(ChangeType.Pop, GameRef.GamePlayScreen);
        }

        public void Begin()
        {
            battleBegin = false;
            MediaPlayer.Play(song);
            combatLog.Clear();

            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();
            monsterImage = Game.Content.Load<Texture2D>(@"Monsters\Monster1");

            Animation animation = new Animation(3, 32, 32, 0, 0);
            animations.Add(AnimationKey.Down, animation);

            animation = new Animation(3, 32, 32, 0, 32);
            animations.Add(AnimationKey.Left, animation);

            animation = new Animation(3, 32, 32, 0, 64);
            animations.Add(AnimationKey.Right, animation);

            animation = new Animation(3, 32, 32, 0, 96);
            animations.Add(AnimationKey.Up, animation);

            AnimatedSprite sprite = new AnimatedSprite(
                monsterImage,
                animations);

            EntityGender gender = EntityGender.Unknown;

            foreach (EntityData monData in DataManager.MonsterData.Values)
            {
                monsterFromData = new Entity("Ghost", monData, gender, EntityType.Monster);
                monsterFromData.Health.SetMaximum(100);
                monsterFromData.Health.SetCurrent(100);
            }

            monster = new Monster(monsterFromData, sprite);

            gamePlayScreenPosition = GamePlayScreen.Player.Sprite.Position;
            gamePlayScreenAnimation = GamePlayScreen.Player.Sprite.CurrentAnimation;
            GamePlayScreen.Player.Sprite.scale = 2.0f;
            monster.Sprite.scale = 2.0f;

            GamePlayScreen.Player.Sprite.Position = new Vector2(GameRef.ScreenRectangle.Width - 75, 500);
            monster.Sprite.Position = new Vector2(25, 500);

            GamePlayScreen.Player.Sprite.CurrentAnimation = AnimationKey.Left;
            monster.Sprite.CurrentAnimation = AnimationKey.Right;
        }

        private string getCombatLogText()
        {
            string combatQueText = string.Empty;

            foreach(string s in combatLog)
            {
                combatQueText += s + "\n";
            }

            return combatQueText;
        }

        private void preformAttack(GameTime gameTime)
        {
            monster.Entity.Health.Damage(50);

            combatLog.Clear();

            combatLog.Enqueue(GamePlayScreen.Player.Character.Entity.EntityName + " hits " + monster.Entity.EntityName + " for 50 damage!");
            timetoDelayCombat -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            GamePlayScreen.Player.Character.Entity.Health.Damage(50);

            combatLog.Enqueue(monster.Entity.EntityName + " hits " + GamePlayScreen.Player.Character.Entity.EntityName + " for 50 damage!");
            timetoDelayCombat -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
      
        #endregion
    }
}
