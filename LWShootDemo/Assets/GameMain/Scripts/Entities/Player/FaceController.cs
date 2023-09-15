using System;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class FaceController : MonoBehaviour
    {
        [SerializeField] 
        private Transform _model;

        public Direction FaceDirection { get; private set; }

        private void Awake()
        {
            FaceDirection = Math.Abs(_model.localScale.x - (-1)) < Single.Epsilon ? Direction.Left : Direction.Right;
        }

        public void Face(Direction direction)
        {
            Debug.Log($"Face {direction}");
            FaceDirection = direction;
            int x =  direction == Direction.Left ? -1 : 1;
            _model.localScale = new Vector3(x, 1, 1);
        }
    }
}