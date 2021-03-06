using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Mover : IMover
{
    private readonly Player m_Player;
    private CharacterController m_CharacterController;
    private IPlayerInput m_Input;

    public Mover(Player mPlayer, IPlayerInput input)
    {
        m_Player = mPlayer;
        m_Input = input;
        m_CharacterController = m_Player.GetComponent<CharacterController>();
        m_Player.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void Tick()
    {
        Vector3 movementInput = new Vector3(m_Input.Horizontal, 0, m_Input.Vertical);
        Vector3 movement = m_Player.transform.rotation * movementInput;
        m_CharacterController.SimpleMove(movement);
    }
}