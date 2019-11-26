using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : IMover
{
    private readonly Player m_Player;
    private NavMeshAgent m_NavMeshAgent;


    public NavMeshMover(Player mPlayer)
    {
        m_Player = mPlayer;
        m_NavMeshAgent = m_Player.GetComponent<NavMeshAgent>();
        m_NavMeshAgent.enabled = true;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out var hitInfo))
            {
                m_NavMeshAgent.SetDestination(hitInfo.point);
            }
        }
    }
}