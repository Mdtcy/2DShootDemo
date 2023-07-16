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

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            // todo 击退力
            var buff1 = GameEntry.TableConfig.Get<BuffTable>().Get(10100000);
            Buff.AddBuff(new AddBuffInfo(buff1, null, this.gameObject, 1, 10, true,true));
        }

        [Button]
        public void TestAddBuff(BuffData buffData)
        {
            var addBuffInfo = new AddBuffInfo(buffData, null, gameObject, 1, 10, true,true);
            AddBuff(addBuffInfo);
        }
    }
}