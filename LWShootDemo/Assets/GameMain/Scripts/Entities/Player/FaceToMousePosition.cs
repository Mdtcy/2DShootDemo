using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class FaceToMousePosition : MonoBehaviour
    {
        [SerializeField] private FaceController _faceController;
        
        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            if (direction.x > 0)
            {
                _faceController.Face(Direction.Right);
            }
            else if (direction.x < 0)
            {
                _faceController.Face(Direction.Left);
            }
        }
    }
}