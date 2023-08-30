using UnityEngine;

namespace GameMain
{
    public class Fruit : EntityLogicBase
    {
        private int _recoverHp;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var fruitData = userData as FruitData;
            _recoverHp = fruitData.RecoverHp;
        }

        protected override void OnRecycle()
        {
            _recoverHp = 0;
            base.OnRecycle();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.GetComponent<Player>();
            if (player != null)
            {
                player.RecoverHp(_recoverHp);
                GameEntry.Entity.HideEntity(this);
            }
        }
    }
}