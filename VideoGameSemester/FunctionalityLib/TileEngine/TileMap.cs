﻿using System;
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
        }

        public TileMap(Tileset tileset, MapLayer layer)
        {
            tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            mapLayers = new List<MapLayer>();
            mapLayers.Add(layer);

            mapWidth = mapLayers[0].Width;
            mapHeight = mapLayers[0].Height;
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