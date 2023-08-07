using UnityEngine;

namespace GameMain
{
    ///<summary>
    ///被挂载的gameobject的记录
    ///</summary>
    public class BindGameObjectInfo
    {
        ///<summary>
        ///gameObject的地址
        ///</summary>
        public GameObject gameObject;

        ///<summary>
        ///还有多少时间之后被销毁，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///有些是不能被销毁的，得外部控制销毁，所以永久存在
        ///</summary>
        public bool forever;

        ///<summary>
        ///<param name="gameObject">要挂载的gameObject</param>
        ///<param name="duration">挂的时间，时间到了销毁，[Magic]如果<=0则代表永久</param>
        ///</summary>
        public BindGameObjectInfo(GameObject gameObject, float duration)
        {
            this.gameObject = gameObject;
            this.duration = Mathf.Abs(duration);
            this.forever = duration <= 0;
        }
    }
}