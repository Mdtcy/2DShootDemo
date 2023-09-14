using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class FaceToVelocityDir : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private FaceController _faceController;

        private void Update()
        {
            if (_rigidbody2D.velocity.x > 0)
            {
                _faceController.Face(Direction.Right);
            }
            else if (_rigidbody2D.velocity.x < 0)
            {
                _faceController.Face(Direction.Left);
            }
        }
    }
}