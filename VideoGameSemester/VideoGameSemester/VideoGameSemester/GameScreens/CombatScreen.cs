using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FunctionalityLib;
using FunctionalityLib.TileEngine;
using FunctionalityLib.SpriteClasses;
using FunctionalityLib.WorldClasses;
using VideoGameSemester.Components;
using FunctionalityLib.Controls;
using Microsoft.Xna.Framework.Content;

namespace VideoGameSemester.GameScreens
{
    public class CombatScreen : BaseGameState
    {
        #region Field Region

        Texture2D backgroundImage;

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

        public override void Initialize()
        {      
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds\Resources\Tower2");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
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
    }
}
