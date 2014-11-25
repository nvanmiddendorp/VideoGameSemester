using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FunctionalityLib.TileEngine
{
    public class TileMap
    {
        #region Field Region

        List<Tileset> tilesets;
        List<MapLayer> mapLayers;
        CollisionLayer collisionLayer;

        static int mapWidth;
        static int mapHeight;

        #endregion

        #region Property Region

        public static int WidthInPixels
        {
            get { return mapWidth * Engine.TileWidth; }
        }

        public static int HeightInPixels
        {
            get { return mapHeight * Engine.TileHeight; }
        }

        #endregion

        #region Constructor Region

        public TileMap(List<Tileset> tilesets, List<MapLayer> layers)
        {
            this.tilesets = tilesets;
            this.mapLayers = layers;

            mapWidth = mapLayers[0].Width;
            mapHeight = mapLayers[0].Height;

            for (int i = 1; i < layers.Count; i++)
            {
                if (mapWidth != mapLayers[i].Width || mapHeight != mapLayers[i].Height)
                    throw new Exception("Map layer size exception");
            }

            collisionLayer = new CollisionLayer(mapWidth, mapHeight);
            
            foreach(MapLayer mapLayer in mapLayers)
            {
                ProcessCollisionLayer(mapLayer);
            }
        }

        public TileMap(Tileset tileset, MapLayer layer)
        {
            tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            mapLayers = new List<MapLayer>();
            mapLayers.Add(layer);

            mapWidth = mapLayers[0].Width;
            mapHeight = mapLayers[0].Height;

            collisionLayer = new CollisionLayer(mapWidth, mapHeight);

            foreach (MapLayer mapLayer in mapLayers)
            {
                ProcessCollisionLayer(mapLayer);
            }
        }

        public TileMap()
        {

        }

        #endregion

        #region Method Region

        public void AddLayer(MapLayer layer)
        {
            if (layer.Width != mapWidth && layer.Height != mapHeight)
                throw new Exception("Map layer size exception");

            mapLayers.Add(layer);
        }

        public void AddTileset(Tileset tileset)
        {
            tilesets.Add(tileset);
        }

        private void ProcessCollisionLayer(MapLayer layer)
        {
            foreach (MapLayer ml in mapLayers)
            {
                if (ml.Name == "CollisionLayer")
                {
                    for (int y = 0; y < mapHeight; y++)
                        for (int x = 0; x < mapWidth; x++)
                        {
                            if (ml.GetTile(x, y).TileIndex != -1)
                                collisionLayer.SetTile(x, y, CollisionType.Unpassable);
                        }
                }
            }
        }

        public bool CheckUpAndLeft(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height));

            bool doesCollide = false;

            if (tile1.X < 0 || tile1.Y < 0)
                return !doesCollide;

            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public bool CheckUp(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width - 1, nextRectangle.Y + nextRectangle.Height));

            bool doesCollide = false;

            if (tile1.Y < 0)
                return !doesCollide;

            int y = tile1.Y;
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public bool CheckUpAndRight(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width + 1, nextRectangle.Y + nextRectangle.Height));

            bool doesCollide = false;

            if (tile2.X >= mapWidth || tile1.Y < 0)
                return !doesCollide;

            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public bool CheckLeft(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height - 1));

            bool doesCollide = false;

            if (tile1.X < 0)
                return !doesCollide;

            int x = tile1.X;
            for (int y = tile1.Y; y <= tile2.Y; y++)
                if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                    doesCollide = true;

            return doesCollide;
        }

        public bool CheckRight(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height - 1));

            bool doesCollide = false;

            if (tile2.X > mapWidth)
                return !doesCollide;

            int x = tile2.X;

            for (int y = tile1.Y; y <= tile2.Y; y++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public bool CheckDownAndLeft(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height));

            bool doesCollide = false;

            if (tile1.X < 0 || tile2.Y >= mapHeight)
                return !doesCollide;

            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public bool CheckDown(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width - 1, nextRectangle.Y + nextRectangle.Height));

            bool doesCollide = false;

            if (tile2.Y >= mapHeight)
                return !doesCollide;

            int y = tile2.Y;
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public bool CheckDownAndRight(Rectangle nextRectangle)
        {
            Point tile1 = Engine.VectorToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = Engine.VectorToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height));

            bool doesCollide = false;

            if (tile2.X >= mapWidth || tile2.Y >= mapHeight)
                return !doesCollide;

            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) == CollisionType.Unpassable)
                        doesCollide = true;

            return doesCollide;
        }

        public CollisionType GetCollisionValue(int x, int y)
        {
            return collisionLayer.GetTile(x, y);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (MapLayer layer in mapLayers)
            {
                layer.Draw(spriteBatch, camera, tilesets);
            }
        }
        #endregion
    }
}