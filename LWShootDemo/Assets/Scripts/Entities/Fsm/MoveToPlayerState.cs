using GameMain;
using LWShootDemo;
using LWShootDemo.Entities;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.StateMachines
{
    [Category("Enemy")]
    public class MoveToPlayerState : FSMState
    {
        public BBParameter<Character> Character;
        public BBParameter<Vector3> Offset;
        public BBParameter<UnitAnimation> UnitAnimation;

        private Transform _player;

        protected override void OnEnter()
        {
            base.OnEnter();
            _player = GameManager.Instance.Player;
            UnitAnimation.value.Play(AnimationType.Walk);
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            Vector2 direction = _player.position + Offset.value  - Character.value.transform.position;
            Character.value.InputMove(direction.normalized);
        }
    }
}