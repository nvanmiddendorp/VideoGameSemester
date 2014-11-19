using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FunctionalityLib;
using FunctionalityLib.Controls;

namespace VideoGameSemester.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        #region Field region

        Texture2D backgroundImage;
        LinkLabel startLabel;
        Label titleLabel;

        #endregion

        #region Constructor region

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #endregion

        #region XNA Method region

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds\Resources\volcano");

            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(350, 600);
            startLabel.Text = "Press ENTER to begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_Selected);

            titleLabel = new Label();
            titleLabel.Position = new Vector2(235, 200);
            titleLabel.Text = "TOTALLY NOT A FINAL FANTASY RIPOFF!";
            titleLabel.Color = Color.White;
            titleLabel.TabStop = false;
            titleLabel.HasFocus = false;

            ControlManager.Add(startLabel);
            ControlManager.Add(titleLabel);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(
                backgroundImage,
                GameRef.ScreenRectangle,
                Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Title Screen Methods

        private void startLabel_Selected(object sender, EventArgs e)
        {
            StateManager.PushState(GameRef.StartMenuScreen);
        }

        #endregion
    }
}