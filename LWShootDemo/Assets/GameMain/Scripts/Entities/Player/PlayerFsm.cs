using System;
using Animancer;
using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class PlayerFsm : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            Run,
            Shoot,
            Die
        }
        
        [SerializeField] 
        private PlayerFsmContext playerFsmContext;
        private StateMachine<PlayerState> stateMachine;

        public void Start()
        {
            stateMachine = new StateMachine<PlayerState>(false);
            stateMachine.AddState(PlayerState.Idle, new IdleSate(false, playerFsmContext));
            // stateMachine.AddState(PlayerState.Shoot, new ShootState(false, playerFsmContext));
            stateMachine.AddState(PlayerState.Run, new RunState(false, playerFsmContext));
            
            stateMachine.SetStartState(PlayerState.Idle);
            
            stateMachine.Init();
        }

        private void FixedUpdate()
        {
            stateMachine.OnLogic();
        }
    }
}