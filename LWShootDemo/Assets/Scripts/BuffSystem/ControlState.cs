using Sirenix.OdinInspector;

namespace LWShootDemo.BuffSystem
{
    public struct ControlState
    {
        ///<summary>
        ///是否可以移动坐标
        ///</summary>
        public bool CanMove;

        ///<summary>
        ///是否可以转身
        ///</summary>
        public bool CanRotate;

        ///<summary>
        ///是否可以使用技能，这里的是“使用技能”特指整个技能流程是否可以开启
        ///如果是类似中了沉默，则应该走buff的onCast，尤其是类似wow里面沉默了不能施法但是还能放致死打击（部分技能被分类为法术，会被沉默，而不是法术的不会）
        ///</summary>
        public bool CanUseSkill;
        
        public ControlState(bool canMove = true, bool canRotate = true, bool canUseSkill = true)
        {
            this.CanMove = canMove;
            this.CanRotate = canRotate;
            this.CanUseSkill = canUseSkill;
        }

        // [Button]
        // public void SetOrigin()
        // {
        //     this.CanMove = true;
        //     this.CanRotate = true;
        //     this.CanUseSkill = true;
        // }
        //
        // [Button]
        // public void SetStun()
        // {
        //     this.CanMove = false;
        //     this.CanRotate = false;
        //     this.CanUseSkill = false;
        // }

        public static ControlState Origin = new ControlState(true, true, true);

        ///<summary>
        ///昏迷效果
        ///</summary>
        public static ControlState Stun = new ControlState(false, false, false);

        public static ControlState operator +(ControlState cs1, ControlState cs2)
        {
            return new ControlState(
                cs1.CanMove & cs2.CanMove,
                cs1.CanRotate & cs2.CanRotate,
                cs1.CanUseSkill & cs2.CanUseSkill
            );
        }
    }
}