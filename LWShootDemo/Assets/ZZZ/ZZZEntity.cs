using ET;
using UnityEngine;

namespace ZZZ
{
    public class ZZZEntity : MonoBehaviour
    {
        private StackFsmComponent stackFsmComponent;
        
        private void Awake()
        {
            stackFsmComponent.ChangeState<IdleState>(StateTypes.Idle, "Idle", 1);
        }
    }
}