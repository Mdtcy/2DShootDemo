using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProjectilePhysicCastComponent : MonoBehaviour
    {
        [SerializeField] 
        private Collider2D _collider2D;

        public void PhysicCast(Vector2 direction, float distance, ref List<RaycastHit2D> hitInfos)
        {
            // todo 可以使用layerMask优化
            // var cf2d = new ContactFilter2D() {useLayerMask = true, useTriggers = true, layerMask = _layerMask};
            var cf2d = new ContactFilter2D() {useLayerMask = true, useTriggers = true};
            
            var bounds = _collider2D.bounds;
            if (_collider2D is CircleCollider2D)
            {
                Physics2D.CircleCast(bounds.center, bounds.extents.y,
                    direction, cf2d, hitInfos, distance);
            }
            else if (_collider2D is BoxCollider2D)
            {
                Physics2D.BoxCast(bounds.center, bounds.size, 0, 
                    direction, cf2d, hitInfos, distance);
            }
            else
            {
                Log.Error("Not support collider type: {0}", _collider2D.GetType().Name);
            }
        }
    }
}