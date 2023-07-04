using GameMain;
using LWShootDemo.Entities;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
namespace NodeCanvas.StateMachines
{
    [Category("Enemy")]
    // [Icon("SomeIconName")] //
    public class AttackState : FSMState 
    {
        public BBParameter<UnitAnimation> UnitAnimation;
        public BBParameter<OldEntity> Entity;
        
        public BBParameter<MeleeAttack> MeleeAttack;

        public BBParameter<float> ss;
        protected override void OnEnter()
        {
            base.OnEnter();
            Entity.value.InputMove(UnityEngine.Vector3.zero);
            UnitAnimation.value.Play(AnimationType.Attack);
            // todo 开启攻击检测 对角色造成伤害
            
            MeleeAttack.value.Attack();
        }
    }
}