using UnityEngine;

namespace GameMain
{
    public class FollowTarget : MonoBehaviour
    {
        public bool TargetExist => _target != null;
        
        private Transform _target;
        private Vector3 _offset;
        
        public void SetTarget(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
        }

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }
            
            transform.position = _target.position + _offset;
        }
    }
}