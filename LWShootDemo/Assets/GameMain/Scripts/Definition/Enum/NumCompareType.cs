using Sirenix.OdinInspector;

namespace GameMain
{
    public enum NumCompareType
    {
        [LabelText("大于")]
        Greater = 0,
        [LabelText("大于等于")]
        GreaterOrEqual = 1,
        [LabelText("等于")]
        Equal = 2,
        [LabelText("小于等于")]
        LessOrEqual = 3,
        [LabelText("小于")]
        Less = 4,
    }
}