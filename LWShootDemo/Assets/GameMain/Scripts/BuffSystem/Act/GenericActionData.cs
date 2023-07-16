using System;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ActionData<T, TAct> : ActionData where T : IEventActArgs where TAct : ActionBase, new()
    {
        string _labelName;
        string LabelName
        {
            get
            {
                if (string.IsNullOrEmpty(_labelName))
                {
                    _labelName = OdinTool.GetLabelText(typeof(TAct));
                }
                return _labelName;
            }
        }

        public override Type ExpectedArgumentType => typeof(T);
        protected override ActionBase CreateActionInternal()
        {
            var act = new TAct();
            act.Initialize(this);
            return act;
        }
    }

}