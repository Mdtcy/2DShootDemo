
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

            _hpBar.Show();
        }
    }
}