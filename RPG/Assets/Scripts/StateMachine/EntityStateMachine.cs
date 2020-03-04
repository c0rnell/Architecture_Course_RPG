using System;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines
{
    public class EntityStateMachine : MonoBehaviour
    {
        private Player m_Player;
        private StateMachine m_StateMachine;
        private NavMeshAgent m_NavMeshAgent;
        private Entity m_Entity;
        public event Action<IState> OnEntityStateChanged;

        public Type CurrentStateType => m_StateMachine.CurrentState.GetType();

        private void Awake()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            m_Player = FindObjectOfType<Player>();
            m_Entity = GetComponent<Entity>();
            
            m_StateMachine = new StateMachine();
            m_StateMachine.OnStateChanged += (state) => OnEntityStateChanged?.Invoke(state);
            
            var idle = new Idle();
            var chasePlayer = new ChasePlayer(m_NavMeshAgent, m_Player);
            var attack = new Attack();
            var dead = new Dead(m_Entity);
            
            m_StateMachine.AddTransition(idle, chasePlayer, () => DistanceFlat(m_NavMeshAgent.transform.position, m_Player.transform.position) < 5f);
            m_StateMachine.AddTransition(chasePlayer, attack, () => DistanceFlat(m_NavMeshAgent.transform.position, m_Player.transform.position) < 2f);
            m_StateMachine.AddAnyTransition(dead, () => m_Entity.Health <= 0);
            
            m_StateMachine.SetState(idle);
        }

        private float DistanceFlat(Vector3 source, Vector3 destination)
        {
            return Vector3.Distance(new Vector3(source.x, 0, source.z), new Vector3(destination.x, 0, destination.z));
        }

        private void Update()
        {
            m_StateMachine.Tick();
        }
    }
}