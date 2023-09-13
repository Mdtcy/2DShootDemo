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
        /// <param name="checkRadius"></param>
        /// <param name="maxAttempts">避免无限循环</param>
        /// <param name="collisionLayer">设置此项以检测特定的碰撞层</param>
        /// <returns></returns>
        public static Vector3Int? FindPositionWithoutCollider(Tilemap tilemap, 
            float checkRadius, 
            LayerMask collisionLayer,
            int maxAttempts = 1000)
        {
            BoundsInt bounds = tilemap.cellBounds;
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
            
            return null; // 如果没有找到合适的位置
        }

        // public static bool IsInsideAnyCompositeCollider(Vector3 pos)
        // {
        //     var compositeCollider2Ds = Object.FindObjectsOfType<CompositeCollider2D>();
        //     foreach (var compositeCollider in compositeCollider2Ds)
        //     {
        //         if (compositeCollider.OverlapPoint(pos))
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
    }
}