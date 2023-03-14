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
        [LabelText("击中声")]
        Hit,
        [LabelText("爆炸声")]
        Explosion,
        [LabelText("按钮点击声")]
        ButtonClick,
        [LabelText("菜单背景音乐")]
        MenuMusic
    }
}
#pragma warning restore 0649