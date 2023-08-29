using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("修改属性PerStack")]
    public class ChangeAttrOnBuffModStackAction : ActionBase<BuffModStackArgs, ChangeAttrOnBuffModStackActData>
    {
        protected override void ExecuteInternal(BuffModStackArgs args)
        {
            foreach (var attrValue in Data.AttrValues)
            {
                args.Buff.Carrier.GetComponent<Character>().UpdateAttr(attrValue.Type, attrValue.Value * args.ModStack);
            }
        }
    }
}