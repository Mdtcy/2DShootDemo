using BerserkPixel.Tilemap_Generator.SO;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace BerserkPixel.Tilemap_Generator.Jobs
{
    public class CellularAutomataJob : MapGenerationJob<CellularMapConfigSO>
    {
        public override MapArray GenerateNoiseMap(CellularMapConfigSO mapConfig)
        {
            var dimensions = new int2(mapConfig.width, mapConfig.height);
            var fillPercent = mapConfig.fillPercent;
            var seed = mapConfig.seed;
            var iterations = mapConfig.smoothSteps;

            var width = dimensions.x;
            var height = dimensions.y;

            var keyLength = width * height;

            var existing = MapArrayExt.GetInitialRandomMap(
                seed, width, height, fillPercent
            );

            var jobResult = new NativeArray<int>(keyLength, Allocator.TempJob);

            if (iterations == 0)
            {
                jobResult.Dispose();
                jobResult = existing;
            }
            else
            {
                for (var i = 1; i <= iterations; i++)
                {
                    var job = new JCellularAutomata
                    {
                        Dimensions = dimensions,
                        Existing = existing,
                        Result = jobResult
                    };

                    job.Schedule(jobResult.Length, 32)
                        .Complete();

                    existing.Dispose();
                    existing = jobResult;

                    if (i != iterations) jobResult = new NativeArray<int>(keyLength, Allocator.TempJob);
                }
            }

            var terrainMap = jobResult.GetMap(width, height);

            jobResult.Dispose();

            return terrainMap;
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    internal struct JCellularAutomata : IJobParallelFor
    {
        public int2 Dimensions;
        [ReadOnly] public NativeArray<int> Existing;

        [WriteOnly] public NativeArray<int> Result;

        public void Execute(int index)
        {
            var x = index % Dimensions.x;
            var y = index / Dimensions.y;

            var isInArray = Existing.Contains(index);

            var neighbors = GetNumberOfNeighbors(x, y);

            if (neighbors > 4)
                Result[index] = MapGeneratorConst.TERRAIN_TILE;
            else if (neighbors < 4)
                Result[index] = MapGeneratorConst.DEFAULT_TILE;
            else if (isInArray)
                Result[index] = Existing[index];
            else
                Result[index] = MapGeneratorConst.DEFAULT_TILE;
        }

        private int GetNumberOfNeighbors(int x, int y)
        {
            var wallCount = 0;

            for (var neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
            for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
                if (neighbourX >= 0 && neighbourX < Dimensions.x && neighbourY >= 0 &&
                    neighbourY < Dimensions.y)
                {
                    if (neighbourX != x || neighbourY != y)
                    {
                        var key = neighbourY * Dimensions.x + neighbourX;
                        wallCount += Existing[key];
                    }
                }
                else
                {
                    wallCount++;
                }

            return wallCount;
        }
    }
}