using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProjectilePhysicCastComponent : MonoBehaviour
    {
        [SerializeField] 
        private Collider2D _collider2D;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void PhysicCast(Vector2 direction, float distance, ref List<RaycastHit2D> hitInfos)
        {
            _direction = direction;
            _distance = distance;
            // todo 可以使用layerMask优化
            // var cf2d = new ContactFilter2D() {useLayerMask = true, useTriggers = true, layerMask = _layerMask};
            var cf2d = new ContactFilter2D() {useLayerMask = false, useTriggers = true};
            
            var bounds = _collider2D.bounds;
            if (_collider2D is CircleCollider2D)
            {
                _spriteRenderer.size = Vector2.one * bounds.extents.y * 2;
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

        private Vector2 _direction;
        private float _distance;

        void OnDrawGizmos()
        {
            if (_collider2D == null)
                return;

            var bounds = _collider2D.bounds;
            var startCenter = bounds.center;
            var endCenter = startCenter + (Vector3)_direction.normalized * _distance;

            Gizmos.color = Color.red;

            if (_collider2D is CircleCollider2D)
            {
                // 可视化CircleCast的起始圆
                Gizmos.DrawWireSphere(startCenter, bounds.extents.y);
                // 可视化CircleCast的方向和距离
                Gizmos.DrawLine(startCenter, endCenter);
            }
            else if (_collider2D is BoxCollider2D)
            {
                // 可视化BoxCast的起始盒子
                Gizmos.DrawWireCube(startCenter, bounds.size);
                // 可视化BoxCast的方向和距离
                Gizmos.DrawLine(startCenter, endCenter);
            }
        }

    }
}