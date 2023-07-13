using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;
using Sirenix.OdinInspector;

namespace GameMain
{
    public class Player : Character
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GetComponent<PlayerController>().Init(this);
            GetComponent<PlayerFsmContext>().Character = this;
        }

        [Button]
        public void TestAddBuff(BuffData buffData)
        {
            var addBuffInfo = new AddBuffInfo(buffData, null, gameObject, 1, 10, true,true);
            AddBuff(addBuffInfo);
        }
    }
}