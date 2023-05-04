using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class PlayerFsm : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            Walk,
            Shoot,
            Die
        }

        private StateMachine<PlayerFsm> stateMachine;

        public void Start()
        {
            stateMachine = new StateMachine<PlayerFsm>();
            // stateMachine.AddState(PlayerState.Idle, new IdleSate());
        }
    }
}