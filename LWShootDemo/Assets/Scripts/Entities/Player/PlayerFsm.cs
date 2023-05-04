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

        [SerializeField] private AnimancerComponent animancerComponent;
        [SerializeField] private Rigidbody2D rb2d;
        
        
        private StateMachine<PlayerState> stateMachine;

        public void Start()
        {
            stateMachine = new StateMachine<PlayerState>(false);
            stateMachine.AddState(PlayerState.Idle, new IdleSate(false, animancerComponent));
            stateMachine.AddState(PlayerState.Shoot, new ShootState(false, animancerComponent));
            stateMachine.AddState(PlayerState.Run, new RunState(false, animancerComponent, rb2d));
            
            stateMachine.SetStartState(PlayerState.Idle);
            
            stateMachine.Init();
        }

        private void FixedUpdate()
        {
            stateMachine.OnLogic();
        }
    }
}