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
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            m_Mover = new Mover(this);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            m_Mover = new NavMeshMover(this);
        
        m_Mover.Tick();
        m_Rotator.Tick();
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