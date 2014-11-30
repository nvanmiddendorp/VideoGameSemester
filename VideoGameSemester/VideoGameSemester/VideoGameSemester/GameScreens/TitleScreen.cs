using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using FunctionalityLib;
using FunctionalityLib.Controls;

namespace VideoGameSemester.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        #region Field region

        Texture2D backgroundImage;
        Texture2D textBoxTitle;
        Texture2D textBoxEnter;
        Rectangle textBoxTitleRectangle;
        Rectangle textBoxEnterRectangle;
        LinkLabel startLabel;
        Label titleLabel;
        Song song;

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

            song = Content.Load<Song>(@"Music\Dungeon9");
            MediaPlayer.Play(song);

            base.LoadContent();

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds\Resources\Tower2");
            textBoxTitle = Content.Load<Texture2D>(@"GUI\textbox");
            textBoxEnter = Content.Load<Texture2D>(@"GUI\textbox");

            textBoxTitleRectangle = new Rectangle((GameRef.ScreenRectangle.Width) - (GameRef.ScreenRectangle.Width), 100, GameRef.ScreenRectangle.Width, 40);
            textBoxEnterRectangle = new Rectangle((GameRef.ScreenRectangle.Width / 2) - (GameRef.ScreenRectangle.Width / 4), (GameRef.ScreenRectangle.Height / 2) + (GameRef.ScreenRectangle.Height / 4), (GameRef.ScreenRectangle.Width / 2), 40);

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2((GameRef.ScreenRectangle.Width / 2) - 160, (GameRef.ScreenRectangle.Height / 2) + (GameRef.ScreenRectangle.Height / 4));
            startLabel.Text = "Press ENTER to begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_Selected);

            titleLabel = new Label();
            titleLabel.Position = new Vector2(GameRef.ScreenRectangle.Width/2 - 295, 100);
            titleLabel.Text = "TOTALLY NOT A FINAL FANTASY RIPOFF!";
            titleLabel.Color = Color.Black;
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

            GameRef.SpriteBatch.Draw(
                textBoxEnter,
                textBoxEnterRectangle,
                Color.Snow);

            GameRef.SpriteBatch.Draw(
                textBoxTitle,
                textBoxTitleRectangle,
                Color.Snow);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Title Screen Methods

        private void startLabel_Selected(object sender, EventArgs e)
        {
            Transition(ChangeType.Push, GameRef.StartMenuScreen);
        }

        #endregion
    }
}