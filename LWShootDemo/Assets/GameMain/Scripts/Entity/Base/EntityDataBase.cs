//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace GameMain
{
    [Serializable]
    public abstract class EntityDataBase
    {
        [SerializeField]
        private EntityProp m_EntityProp;

        [SerializeField]
        private Vector3 m_Position = Vector3.zero;

        [SerializeField] 
        private Vector3 m_Scale = Vector3.one;
        

        [SerializeField]
        private Quaternion m_Rotation = Quaternion.identity;

        public EntityDataBase(EntityProp entityProp)
        {
            m_EntityProp = entityProp;
        }

        public EntityProp EntityProp
        {
            get
            {
                return m_EntityProp;
            }
        }

        /// <summary>
        /// 实体位置。
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        /// <summary>
        /// 实体朝向。
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return m_Rotation;
            }
            set
            {
                m_Rotation = value;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return m_Scale;
            }
            set
            {
                m_Scale = value;
            }
        }
    }
}