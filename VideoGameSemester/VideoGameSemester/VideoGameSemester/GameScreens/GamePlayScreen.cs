using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using FunctionalityLib;
using FunctionalityLib.TileEngine;
using FunctionalityLib.SpriteClasses;
using FunctionalityLib.WorldClasses;
using VideoGameSemester.Components;

namespace VideoGameSemester.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        #region Field Region

        Engine engine = new Engine(32, 32);
        static Player player;
        static World world;

        //combat screen rolls
        Random random = new Random();
        int range = 100;
        int roll;

        Song song;

        #endregion

        #region Property Region

        public static World World
        {
            get { return world; }
            set { world = value; }
        }

        public static Player Player
        {
            get { return player; }
            set { player = value; }
        }

        #endregion

        #region Constructor Region
        
        public GamePlayScreen(Game game, GameStateManager manager)
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
            ContentManager Content = GameRef.Content;

            song = Content.Load<Song>(@"Music\Darkened Winds");
            MediaPlayer.Play(song);
            base.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            world.Update(gameTime);
            player.Update(gameTime);
            
            if(player.Sprite.IsAnimating)
            {
                roll = random.Next(1, range);
                if(roll == 1)
                {
                    CombatScreen.battleBegin = true;
                    Transition(ChangeType.Push, GameRef.CombatScreen);
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                player.Camera.Transformation);

            base.Draw(gameTime);

            world.DrawLevel(GameRef.SpriteBatch, player.Camera);
            player.Draw(gameTime, GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Abstract Method Region
        public bool CheckUnwalkableTile(AnimatedSprite sprite, Vector2 motion)
        {
            List<Level> levels = World.Levels;
            TileMap tileMap = levels.ElementAt(world.CurrentLevel).Map;

            Vector2 nextLocation = sprite.Position + motion;

            Rectangle nextRectangle = new Rectangle((int)nextLocation.X, (int)nextLocation.Y, sprite.Width, sprite.Height);

            if (motion.Y < 0 && motion.X < 0)
            {
                return tileMap.CheckUpAndLeft(nextRectangle);
            }
            else if (motion.Y < 0 && motion.X == 0)
            {
                return tileMap.CheckUp(nextRectangle);
            }
            else if (motion.Y < 0 && motion.X > 0)
            {
                return tileMap.CheckUpAndRight(nextRectangle);
            }
            else if (motion.Y == 0 && motion.X < 0)
            {
                return tileMap.CheckLeft(nextRectangle);
            }
            else if (motion.Y == 0 && motion.X > 0)
            {
                return tileMap.CheckRight(nextRectangle);
            }
            else if (motion.Y > 0 && motion.X < 0)
            {
                return tileMap.CheckDownAndLeft(nextRectangle);
            }
            else if (motion.Y > 0 && motion.X == 0)
            {
                return tileMap.CheckDown(nextRectangle);
            }

            return tileMap.CheckDownAndRight(nextRectangle);
        }

        #endregion
    }
}


