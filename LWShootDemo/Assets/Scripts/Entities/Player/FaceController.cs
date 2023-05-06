using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class FaceController : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;

        public void Face(Direction direction)
        {
            _spriteRenderer.flipX = direction == Direction.Left;
        }
    }
}