using System;
using UnityEngine;

    public abstract class ItemComponent : MonoBehaviour
    {
        protected float m_NextUseTime;
        
        public bool CanUse => Time.time >= m_NextUseTime;

        public abstract void Use();
    }