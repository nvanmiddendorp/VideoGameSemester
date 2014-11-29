using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.WorldClasses
{
    public struct Tileg
    {
        public int TileIndex;
        public int TileSetIndex;

        public Tileg(int tileIndex, int tileSetIndex)
        {
            TileIndex = tileIndex;
            TileSetIndex = tileSetIndex;
        }
    }

    public class MapLayerData
    {
        public string MapLayerName;
        public int Width;
        public int Height;
        public Tileg[] Layer;

        private MapLayerData()
        {
        }

        public MapLayerData(string mapLayerName, int width, int height)
        {
            MapLayerName = mapLayerName;
            Width = width;
            Height = height;

            Layer = new Tileg[height * width];
        }

        public MapLayerData(string mapLayerName, int width, int height, int tileIndex, int tileSet)
        {
            MapLayerName = mapLayerName;
            Width = width;
            Height = height;

            Layer = new Tileg[height * width];

            Tileg tile = new Tileg(tileIndex, tileSet);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    SetTile(x, y, tile);
        }

        public void SetTile(int x, int y, Tileg tile)
        {
            Layer[y * Width + x] = tile;
        }

        public void SetTile(int x, int y, int tileIndex, int tileSet)
        {
            Layer[y * Width + x] = new Tileg(tileIndex, tileSet);
        }

        public Tileg GetTile(int x, int y)
        {
            return Layer[y * Width + x];
        }
    }
}
