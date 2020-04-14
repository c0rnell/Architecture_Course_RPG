using UnityEngine;

public class Player : MonoBehaviour
{
    public float RotateSpeed => m_RotateSpeed;

    private IMover m_Mover;

    [SerializeField]
    private float m_RotateSpeed = 10;
    private Rotator m_Rotator;

    private void Awake()
    {
        m_Mover = new Mover(this, PlayerInput.Instance);
        m_Rotator = new Rotator(this, PlayerInput.Instance);

        PlayerInput.Instance.MoveModeTogglePressed += OnMoveModeToggle;
    }

    public void OnMoveModeToggle()
    {
        if (m_Mover is NavMeshMover)
            m_Mover = new Mover(this, PlayerInput.Instance);
        else
            m_Mover = new NavMeshMover(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Pause.Active)
            return;
        
        m_Mover.Tick();
        m_Rotator.Tick();
    }
}