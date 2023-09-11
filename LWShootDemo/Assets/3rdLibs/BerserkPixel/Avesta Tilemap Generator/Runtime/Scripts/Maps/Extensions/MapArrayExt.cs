using System;
using BerserkPixel.Tilemap_Generator.SO;
using Unity.Collections;

namespace BerserkPixel.Tilemap_Generator
{
    public static class MapArrayExt
    {
        public static MapArray GetMap(this NativeArray<int> jobResult, int width, int height)
        {
            var terrainMap = new MapArray(width, height);

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var key = terrainMap.Key(x, y);

                terrainMap[x, y] = jobResult[key];
            }

            return terrainMap;
        }

        public static NativeArray<int> GetInitialRandomMap(string seed, int width, int height, float fillPercent,
            bool invert = false)
        {
            var terrainMap = new NativeArray<int>(width * height, Allocator.TempJob);
            var pseudoRandom = new Random(seed.GetHashCode());

            // we fill with random variables
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var key = y * width + x;
                    terrainMap[key] = pseudoRandom.Next(0, 100).GetTile(100 - fillPercent, invert);
                }
            }

            return terrainMap;
        }

        public static MapArray FullDefaultMap(RandomWalkSO mapConfig)
        {
            var terrainMap = new MapArray(mapConfig.width, mapConfig.height);

            for (var y = 0; y < mapConfig.height; y++)
            for (var x = 0; x < mapConfig.width; x++)
                terrainMap[x, y] = mapConfig.invert ? MapGeneratorConst.TERRAIN_TILE : MapGeneratorConst.DEFAULT_TILE;

            return terrainMap;
        }

        public static MapArray FullDefaultMap(PathSO mapConfig)
        {
            var terrainMap = new MapArray(mapConfig.width, mapConfig.height);

            for (var y = 0; y < mapConfig.height; y++)
            for (var x = 0; x < mapConfig.width; x++)
                terrainMap[x, y] = mapConfig.invert ? MapGeneratorConst.TERRAIN_TILE : MapGeneratorConst.DEFAULT_TILE;

            return terrainMap;
        }
    }
}