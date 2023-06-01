using System;
using rStarTools.Scripts.StringList;
using Sirenix.OdinInspector;

namespace LWShootDemo.BuffSystem.Tags
{
    [Serializable]
    public class BuffTagData : DataBase<BuffTagOverview>
    {
        #region Public Variables

        [LabelText("資料棄用:")]
        [HorizontalGroup("BuffTagData")]
        [LabelWidth(55)]
        public bool Deactivate;

        #endregion
    }
}