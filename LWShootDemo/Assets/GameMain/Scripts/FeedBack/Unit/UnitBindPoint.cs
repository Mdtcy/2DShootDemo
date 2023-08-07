using UnityEngine;
using System.Collections.Generic;
using UnityGameFramework.Runtime;

namespace GameMain
{
    ///<summary>
    ///在一个gameObject下添加这个，让这个gameObject成为一个“绑点”，这样就可以在这东西里面管理一些挂载的gameObject
    ///最常见的用途是角色身上某个点播放视觉特效什么的。
    ///</summary>
    public class UnitBindPoint : MonoBehaviour
    {
        ///<summary>
        ///绑点的名称
        ///</summary>
        public string key;

        ///<summary>
        ///偏移坐标
        ///</summary>
        public Vector3 offset;

        ///<summary>
        ///已经挂着的gameobject信息
        ///key就是一个索引，便于找到
        ///</summary>
        private Dictionary<string, BindGameObjectInfo> bindGameObject = new();

        private void FixedUpdate()
        {
            List<string> toRemove = new List<string>();
            foreach (KeyValuePair<string, BindGameObjectInfo> goInfo in bindGameObject)
            {
                if (goInfo.Value.gameObject == null)
                {
                    toRemove.Add(goInfo.Key);
                    continue;
                }

                if (goInfo.Value.forever == false)
                {
                    goInfo.Value.duration -= Time.fixedDeltaTime;
                    if (goInfo.Value.duration <= 0)
                    {
                        Destroy(goInfo.Value.gameObject);
                        toRemove.Add(goInfo.Key);
                    }
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                bindGameObject.Remove(toRemove[i]);
            }
        }

        ///<summary>
        ///添加一个gameObject绑定
        ///<param name="goPath">要挂载的gameObject的prefabs路径，必须在resources下</param>
        ///<param name="key">挂载信息的key，其实就是dictionary的key，手动删除的时候要用</param>
        ///<param name="loop">是否循环播放，直到手动删除</param>
        ///</summary>
        public void AddBindGameObject(string goPath, string key, bool loop)
        {
            // //已经存在，加不成
            if (key != "" && bindGameObject.ContainsKey(key) == true)
            {
                Log.Error("AddBindGameObject key is exist");
                return;
            } 

            GameObject effectGO = Instantiate<GameObject>(
                Resources.Load<GameObject>(goPath),
                Vector3.zero,
                Quaternion.identity,
                this.gameObject.transform
            );
            effectGO.transform.localPosition = this.offset;
            effectGO.transform.localRotation = Quaternion.identity;
            if (!effectGO)
            {
                Log.Error("AddBindGameObject effectGO is null");
                return;
            }

            SightEffect se = effectGO.GetComponent<SightEffect>();
            if (!se)
            {
                Destroy(effectGO);
                return;
            }

            float duration = se.duration * (loop == false ? 1 : -1);
            BindGameObjectInfo bindGameObjectInfo = new BindGameObjectInfo(
                effectGO, duration
            );
            if (key != "")
            {
                this.bindGameObject.Add(key, bindGameObjectInfo);
            }
            else
            {
                this.bindGameObject.Add(
                    // todo 
                    Time.frameCount * Random.Range(1.00f, 9.99f) + "_" + Random.Range(1, 9999),
                    bindGameObjectInfo
                );
            }

        }

        ///<summary>
        ///移除一个gameObject的绑定
        ///<param name="key">挂载信息的key，其实就是dictionary的key</param>
        ///</summary>
        public void RemoveBindGameObject(string key)
        {
            if (bindGameObject.ContainsKey(key) == false)
            {
                return;
            }

            if (bindGameObject[key].gameObject)
            {
                Destroy(bindGameObject[key].gameObject);
            }

            bindGameObject.Remove(key);
        }
    }
}