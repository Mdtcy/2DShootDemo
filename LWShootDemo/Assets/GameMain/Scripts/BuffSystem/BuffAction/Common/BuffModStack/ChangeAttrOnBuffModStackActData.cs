using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace GameMain
{
    public class ChangeAttrOnBuffModStackActData : ActionData<BuffModStackArgs, ChangeAttrOnBuffModStackAction>
    {
        [TableList]
        public List<NumericValue> AttrValues = new();
    }
}