
using GameMain.Item;
using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;
using LWShootDemo.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class Player : Character
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GetComponent<PlayerController>().Init(this);
            GetComponent<PlayerFsmContext>().Character = this;
            
            // todo 
            _weapon = GetComponentInChildren<Weapon>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            _hpBar.Show();
            
        }

        private float lastShotTime;
        private Weapon _weapon;
        private float _fireRate = 0.4f;

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            // 武器朝向敌人
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 获取敌人方向
            var direction = mousePos - transform.position;
            
            _weapon.RotateTo(direction);
            bool canShoot = lastShotTime + _fireRate < Time.time;
            if (canShoot && Input.GetMouseButton(0))
            {
                direction.z = 0;

                // 使用武器
                _weapon.Use();
                lastShotTime = Time.time;
            }
        }

        public void PickItem(ItemProp itemProp)
        {
            AddBuff(new AddBuffInfo(itemProp.Buff, 
                null, 
                gameObject,
                stack: 1, 
                duration: 10, 
                durationSetTo:true,
                permanent: true));
        }
    }
}