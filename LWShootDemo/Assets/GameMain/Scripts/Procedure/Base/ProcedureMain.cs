using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProcedureMain : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Debug("Enter ProcedureMain.");
            GameEntry.Projectile.Init();

            GameEntry.Entity.ShowEnemyGhoul(new EnemyGhoulData(GameEntry.Entity.GenerateSerialId(), 20001)
            {
                Position = new Vector3(0, 0, 0),
                Rotation = Quaternion.identity,
                Scale = Vector3.one,
            });
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameEntry.Entity.ShowEnemyGhoul(new EnemyGhoulData(GameEntry.Entity.GenerateSerialId(), 20001)
                {
                    Position = new Vector3(0, 0, 0),
                    Rotation = Quaternion.identity,
                    Scale = Vector3.one,
                });
            }
        }
    }
}