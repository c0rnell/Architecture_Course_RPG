using UnityEngine;

[RequireComponent(typeof(Entity))]
public class EntityAnimator : MonoBehaviour
{
    private Animator m_Animator;
    private Entity m_Entity;
    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Entity = GetComponent<Entity>();

        m_Entity.OnDied += () => m_Animator.SetBool(Die, true);
    }
}
