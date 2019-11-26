using UnityEngine;
using UnityEngine.AI;

public class Mover : IMover
{
    private readonly Player m_Player;
    private CharacterController m_CharacterController;

    public Mover(Player mPlayer)
    {
        m_Player = mPlayer;
        m_CharacterController = m_Player.GetComponent<CharacterController>();
        m_Player.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void Tick()
    {
        Vector3 movementInput = new Vector3(m_Player.PlayerInput.Horizontal, 0, m_Player.PlayerInput.Vertical);
        Vector3 movement = m_Player.transform.rotation * movementInput;
        m_CharacterController.SimpleMove(movement);
    }
}