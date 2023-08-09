using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [Serializable]
    public abstract class ActionData
    {
        [TitleGroup("@LabelName", alignment: TitleAlignments.Centered)] 
        [GUIColor(nameof(GetStateColor))]
        public ActionState State = ActionState.Enable;

        public abstract Type ExpectedArgumentType { get; }

        protected abstract ActionBase CreateActionInternal();
        
        public ActionBase CreateAction()
        {
            return CreateActionInternal();
        }
        
        private Color GetStateColor()
        {
            switch (State)
            {
                case ActionState.Disable:
                    return Color.red;
                case ActionState.Enable:
                default:
                    return Color.white;
            }
        }
    }
}