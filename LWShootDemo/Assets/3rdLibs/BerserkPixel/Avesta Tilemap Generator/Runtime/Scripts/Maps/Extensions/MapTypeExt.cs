using BerserkPixel.Tilemap_Generator.Algorithms;
using BerserkPixel.Tilemap_Generator.SO;

namespace BerserkPixel.Tilemap_Generator
{
    public static class MapTypeExt
    {
        public static MapType GetFromSO(this MapConfigSO mapConfig)
        {
            var selectedType = mapConfig.GetType();

            if (selectedType == typeof(CellularMapConfigSO)) return MapType.CellularAutomata;

            if (selectedType == typeof(PerlinNoiseMapConfigSO)) return MapType.PerlinNoise;

            if (selectedType == typeof(GameOfLifeMapConfigSO)) return MapType.GameOfLife;

            if (selectedType == typeof(BasicRandomSO)) return MapType.BasicRandom;

            if (selectedType == typeof(RandomWalkSO)) return MapType.RandomWalk;

            if (selectedType == typeof(PathSO)) return MapType.Path;

            if (selectedType == typeof(IslandConfigSO)) return MapType.Island;

            if (selectedType == typeof(FractalNoiseConfigSO)) return MapType.FractalNoise;

            if (selectedType == typeof(DomainWarpingSO)) return MapType.DomainWarping;

            if (selectedType == typeof(RidgedMultifractalSO)) return MapType.RidgedMultifractal;

            if (selectedType == typeof(WaveletNoiseSO)) return MapType.WaveletNoise;

            if (selectedType == typeof(FourierNoiseSO)) return MapType.FourierNoise;

            if (selectedType == typeof(VoronoiSO)) return MapType.Voronoi;

            // by default it's a full grid type
            return MapType.FullGrid;
        }

        public static IMapAlgorithm GetFromType(this MapType mapType, MapConfigSO mapConfig)
        {
            switch (mapType)
            {
                case MapType.CellularAutomata:
                    return new CellularAutomata(mapConfig);
                case MapType.PerlinNoise:
                    return new PerlinNoise(mapConfig);
                case MapType.GameOfLife:
                    return new GameOfLife(mapConfig);
                case MapType.BasicRandom:
                    return new BasicRandom(mapConfig);
                case MapType.RandomWalk:
                    return new RandomWalk(mapConfig);
                case MapType.Path:
                    return new Path(mapConfig);
                case MapType.Island:
                    return new Island(mapConfig);
                case MapType.FractalNoise:
                    return new FractalNoise(mapConfig);
                case MapType.DomainWarping:
                    return new DomainWarping(mapConfig);
                case MapType.RidgedMultifractal:
                    return new RidgedMultifractal(mapConfig);
                case MapType.WaveletNoise:
                    return new WaveletNoise(mapConfig);
                case MapType.FourierNoise:
                    return new FourierNoise(mapConfig);
                case MapType.Voronoi:
                    return new VoronoiNoise(mapConfig);
                case MapType.FullGrid:
                default:
                    // by default it's a full grid type
                    return new FullGrid(mapConfig);
            }
        }
    }
}