using UnityEngine;

public class Rotator
{
    private readonly Player m_Player;
    private IPlayerInput m_Input;

    public Rotator(Player player, IPlayerInput input)
    {
        m_Player = player;
        m_Input = input;
    }

    public void Tick()
    {
        var rotation = new Vector3(0, m_Input.MouseX * m_Player.RotateSpeed, 0);
        m_Player.transform.Rotate(rotation);
    }
}