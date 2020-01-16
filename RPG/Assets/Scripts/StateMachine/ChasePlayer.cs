using UnityEngine.AI;

namespace StateMachines
{
    public class ChasePlayer : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Player m_Palyer;

        public ChasePlayer(NavMeshAgent navMeshAgent, Player mPlayer)
        {
            _navMeshAgent = navMeshAgent;
            m_Palyer = mPlayer;
        }
        
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(m_Palyer.transform.position);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }

        public void Tick()
        {
        }
    }
}