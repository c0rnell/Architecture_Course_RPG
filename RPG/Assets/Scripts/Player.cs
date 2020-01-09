using UnityEngine;

public class Player : MonoBehaviour
{
    public IPlayerInput PlayerInput { get; set; } = new PlayerInput();

    private IMover m_Mover;
    private Rotator m_Rotator;

    private void Awake()
    {
        m_Mover = new Mover(this);
        m_Rotator = new Rotator(this);

        PlayerInput.MoveModeTogglePressed += OnMoveModeToggle;
    }

    public void OnMoveModeToggle()
    {
        if (m_Mover is NavMeshMover)
            m_Mover = new Mover(this);
        else
            m_Mover = new NavMeshMover(this);
    }

    // Update is called once per frame
    private void Update()
    {
        m_Mover.Tick();
        m_Rotator.Tick();
        PlayerInput.Tick();
    }
}

public class Rotator
{
    private readonly Player m_Player;
    
    public Rotator(Player player)
    {
        m_Player = player;
    }

    public void Tick()
    {
        var rotation = new Vector3(0, m_Player.PlayerInput.MouseX, 0);
        m_Player.transform.Rotate(rotation);
    }
}