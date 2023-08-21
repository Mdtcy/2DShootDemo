using System;
using UnityEngine;

namespace GameMain
{
    public class AoeTickDamageAction : ActionBase<OnAoeTickArgs, AoeTickDamageActionData>
    {
        protected override void ExecuteInternal(OnAoeTickArgs args)
        {
            var player = (GameEntry.Procedure.CurrentProcedure as ProcedureMain).Player.Logic as Character;
            float damage = player.Attack * Data.Percent;
            
            // todo 
            throw new NotImplementedException();
        }
    }
}