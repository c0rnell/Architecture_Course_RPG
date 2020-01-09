using UnityEngine.AI;

namespace StateMachines
{
    public class ChasePlayer : IState
    {
        private readonly NavMeshAgent _navMeshAgent;

        public ChasePlayer(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }
        
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
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