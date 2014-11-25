using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FunctionalityLib;
using FunctionalityLib.Controls;
using FunctionalityLib.SpriteClasses;
using FunctionalityLib.TileEngine;
using FunctionalityLib.WorldClasses;
using VideoGameSemester.Components;
using GameLib.WorldClasses;

namespace VideoGameSemester.GameScreens
{
    public class CharacterGeneratorScreen : BaseGameState
    {
        #region Field Region

        LeftRightSelector characterSelector;
        PictureBox backgroundImage;

        PictureBox characterImage;
        Texture2D[] characterImages;

        string[] characters = { "Actor1", "Actor2", "Actor3", "Actor4", "Actor5" };

        #endregion

        #region Property Region

        public string SelectedGender
        {
            get { return characterSelector.SelectedItem; }
        }

        #endregion

        #region Constructor Region

        public CharacterGeneratorScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
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
            base.LoadContent();

            LoadImages();
            CreateControls();
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

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Method Region

        private void CreateControls()
        {
            Texture2D leftTexture = Game.Content.Load<Texture2D>(@"GUI\leftarrowUp");
            Texture2D rightTexture = Game.Content.Load<Texture2D>(@"GUI\rightarrowUp");
            Texture2D stopTexture = Game.Content.Load<Texture2D>(@"GUI\StopBar");

            backgroundImage = new PictureBox(
                Game.Content.Load<Texture2D>(@"Backgrounds\Resources\Tower2"),
                GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);

            Label label1 = new Label();

            label1.Text = "Who will defeat the Professors?";
            label1.Size = label1.SpriteFont.MeasureString(label1.Text);
            label1.Position = new Vector2((GameRef.Window.ClientBounds.Width - label1.Size.X) / 2, 150);

            ControlManager.Add(label1);

            characterSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            characterSelector.SetItems(characters, 125);
            characterSelector.Position = new Vector2(label1.Position.X, 200);
            characterSelector.SelectionChanged += new EventHandler(selectionChanged);

            ControlManager.Add(characterSelector);

            LinkLabel linkLabel1 = new LinkLabel();
            linkLabel1.Text = "Accept this character.";
            linkLabel1.Position = new Vector2(label1.Position.X, 300);
            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);

            ControlManager.Add(linkLabel1);

            characterImage = new PictureBox(
                characterImages[0], 
                new Rectangle(500, 200, 96, 96), 
                new Rectangle(0, 0, 32, 32));
            ControlManager.Add(characterImage);

            ControlManager.NextControl();
        }

        private void LoadImages()
        {
            characterImages = new Texture2D[characters.Length];

            for (int i = 0; i < characters.Length; i++)
            {
                characterImages[i] = Game.Content.Load<Texture2D>(@"PlayerSprites\" + characters[i]);
            }
        }

        void linkLabel1_Selected(object sender, EventArgs e)
        {
            InputHandler.Flush();

            CreatePlayer();
            CreateWorld();

            Transition(ChangeType.Change, GameRef.GamePlayScreen);
        }

        private void CreatePlayer()
        {
            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();

            Animation animation = new Animation(3, 32, 32, 0, 0);
            animations.Add(AnimationKey.Down, animation);

            animation = new Animation(3, 32, 32, 0, 32);
            animations.Add(AnimationKey.Left, animation);

            animation = new Animation(3, 32, 32, 0, 64);
            animations.Add(AnimationKey.Right, animation);

            animation = new Animation(3, 32, 32, 0, 96);
            animations.Add(AnimationKey.Up, animation);

            AnimatedSprite sprite = new AnimatedSprite(
                characterImages[characterSelector.SelectedIndex],
                animations);

            GamePlayScreen.Player = new Player(GameRef, sprite);

            //place the player in the map
            GamePlayScreen.Player.Sprite.Position = new Vector2(200, 200);
        }

        private void CreateWorld()
        {      
            List<Tileset> tilesets = new List<Tileset>();        
            List<MapLayer> mapLayers = new List<MapLayer>();         
            MapData mapData = Game.Content.Load<MapData>(@"Game\Levels\Maps\Village");
            
            foreach (TilesetData tilesetData in mapData.Tilesets)
            {
                string filename = Path.GetFileNameWithoutExtension(tilesetData.TilesetImageName);
                Texture2D texture = Game.Content.Load<Texture2D>(@"Tilesets\Resources\" + filename);
                Tileset tileset = new Tileset(texture, tilesetData.TilesWide, tilesetData.TilesHigh, tilesetData.TileWidthInPixels, tilesetData.TileHeightInPixels);
                tilesets.Add(tileset);
            }

            foreach (MapLayerData mapLayerData in mapData.Layers)
            {
                MapLayer maplayer = MapLayer.FromMapLayerData(mapLayerData);
                mapLayers.Add(maplayer);
            }

            TileMap map = new TileMap(tilesets, mapLayers);
            Level level = new Level(map);

            World world = new World(GameRef, GameRef.ScreenRectangle);
            world.Levels.Add(level);
            world.CurrentLevel = 0;

            GamePlayScreen.World = world;
        }

        void selectionChanged(object sender, EventArgs e)
        {
            characterImage.Image = characterImages[characterSelector.SelectedIndex];
        }

        #endregion
    }
}