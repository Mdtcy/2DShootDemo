using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace GameMain.Scripts.Utility
{
    public static class TilemapUtility
    {
        /// <summary>
        /// 在一个Tilemap中随机找到一个没有碰撞的位置
        /// </summary>
        /// <param name="tilemap"></param>
        /// <param name="xRange"></param>
        /// <param name="yRange"></param>
        /// <param name="checkRadius"></param>
        /// <param name="maxAttempts">避免无限循环</param>
        /// <param name="collisionLayer">设置此项以检测特定的碰撞层</param>
        /// <returns></returns>
        public static Vector3Int? FindPositionWithoutCollider(Tilemap tilemap,
            Vector2Int xRange,
            Vector2Int yRange,
            float checkRadius, 
            LayerMask collisionLayer,
            int maxAttempts = 1000)
        {
            BoundsInt bounds = tilemap.cellBounds;
            bounds.xMin = xRange.x;
            bounds.xMax = xRange.y;
            bounds.yMin = yRange.x;
            bounds.yMax = yRange.y;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Vector3Int randomPosition = new Vector3Int(
                    Random.Range(bounds.xMin, bounds.xMax),
                    Random.Range(bounds.yMin, bounds.yMax),
                    0);

                Vector2 worldPos = tilemap.GetCellCenterWorld(randomPosition);

                if (!Physics2D.OverlapCircle(worldPos, checkRadius, collisionLayer))
                {
                    Log.Debug($"经过{attempts}次查找后找到了正确的位置{randomPosition}");
                    return randomPosition; // 返回找到的位置
                }

                attempts++;
            }
            Log.Warning($"没有找到合适的位置");
            return null; // 如果没有找到合适的位置
        }

        public static Vector3? FindPositionWithoutColliderNearPosition(Tilemap tilemap,
            Vector3 position,
            float radius,
            float checkRadius, 
            LayerMask collisionLayer,
            int maxAttempts = 1000)
        {
            int attempts = 0;
            BoundsInt tileBounds = tilemap.cellBounds; // Tilemap边界
            while (attempts < maxAttempts)
            {
                // 在圆内随机一个位置
                Vector2 randomDirection = Random.insideUnitCircle;
                Vector3 randomPositionWithinRadius = position + (Vector3)(randomDirection * radius);

                // 转换为Tilemap的坐标
                Vector3Int tilePosition = tilemap.WorldToCell(randomPositionWithinRadius);
                Vector3 worldPos = tilemap.GetCellCenterWorld(tilePosition);

                // 确保该位置在Tilemap的边界内
                if (!tileBounds.Contains(tilePosition))
                {
                    attempts++;
                    continue;
                }
                
                if (!Physics2D.OverlapCircle(worldPos, checkRadius, collisionLayer))
                {
                    // Debug.Log($"经过{attempts}次查找后找到了正确的位置{tilePosition}");
                    return worldPos; // 返回找到的位置
                }

                attempts++;
            }
    
            return null; // 如果没有找到合适的位置
        }
    }
}