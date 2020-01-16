using UnityEngine;

namespace StateMachines
{
    public class Dead : IState
    {
        private const float DespawnDelay = 5f;
        
        private float m_DespawnTime;
        private Entity m_Entity;

        public Dead(Entity mEntity)
        {
            m_Entity = mEntity;
        }

        public void OnEnter()
        {
            m_DespawnTime = Time.time + DespawnDelay;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            if (Time.time >= m_DespawnTime)
            {
                GameObject.Destroy(m_Entity.gameObject);
            }
        }
    }
}