/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [音乐类型]
 */

using Sirenix.OdinInspector;

#pragma warning disable 0649

namespace LWShootDemo.Sound
{
    /// <summary>
    /// 音乐类型
    /// </summary>
    public enum SoundType
    {
        [LabelText("开火声")]
        Fire,
        [LabelText("战斗背景音乐")]
        BattleMusic,
    }
}
#pragma warning restore 0649