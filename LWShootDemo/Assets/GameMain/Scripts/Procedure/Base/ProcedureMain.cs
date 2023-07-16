using GameFramework.Fsm;
using GameFramework.Procedure;
using LWShootDemo.Entities;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProcedureMain : ProcedureBase
    {
        public Entity Player => GameEntry.Entity.GetEntity(_playerEntityId);
        
        private int _playerEntityId;
        
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Debug("Enter ProcedureMain.");
            GameEntry.Projectile.Init();
            
            _playerEntityId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowPlayer(new PlayerData(_playerEntityId, 10000)
            {
                Position = new Vector3(0, -1, 0),
                Rotation = Quaternion.identity,
                Scale = Vector3.one,
                Side = Side.Player,
                MaxHp = 10
            });
            GameEntry.Entity.ShowEnemy(new EnemyData(GameEntry.Entity.GenerateSerialId(), 20001)
            {
                Position = new Vector3(0, 3, 0),
                Rotation = Quaternion.identity,
                Scale = Vector3.one,
                Side = Side.Enemy,
                MaxHp = 3
            });
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameEntry.Entity.ShowEnemy(new EnemyData(GameEntry.Entity.GenerateSerialId(), 20001)
                {
                    Position = new Vector3(0, 0, 0),
                    Rotation = Quaternion.identity,
                    Scale = Vector3.one,
                    Side = Side.Enemy,
                    MaxHp = 3
                });
            }
        }
    }
}