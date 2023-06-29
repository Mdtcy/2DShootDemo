using System;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class FaceController : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;

        public Direction FaceDirection { get; private set; }

        private void Awake()
        {
            FaceDirection = _spriteRenderer.flipX ? Direction.Left : Direction.Right;
        }

        public void Face(Direction direction)
        {
            FaceDirection = direction;
            _spriteRenderer.flipX = direction == Direction.Left;
        }
    }
}