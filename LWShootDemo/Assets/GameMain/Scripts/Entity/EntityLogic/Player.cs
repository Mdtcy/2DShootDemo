using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;

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
    }
}