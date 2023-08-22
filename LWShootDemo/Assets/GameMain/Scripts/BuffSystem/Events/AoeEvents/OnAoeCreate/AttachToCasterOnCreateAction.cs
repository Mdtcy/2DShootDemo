using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    public class AttachToCasterOnCreateAction : ActionBase<OnAoeCreateArgs, AttachToCasterOnCreateActionData>
    {
        protected override void ExecuteInternal(OnAoeCreateArgs args)
        {
            var caster = args.Aoe.Caster;
            Assert.IsNotNull(caster);
            GameEntry.Entity.AttachEntity(args.Aoe.Id,caster.Id);
        }
    }
}