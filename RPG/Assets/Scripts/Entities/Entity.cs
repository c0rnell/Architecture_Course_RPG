using System;
using UnityEngine;

public class Entity : MonoBehaviour, ITakeHits
{
    [SerializeField] private int m_MaxHealth;
    public event Action OnDied;

    public int Health { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Health = m_MaxHealth;
    }

    public void TakeHit(int amount)
    {
        Health -= amount;
        if (Health <= 0)
            Die();
        else
            HandleNonLethalHit();
    }

    private void HandleNonLethalHit()
    {
        Debug.Log("Took Non-lethal hit");
    }

    private void Die()
    {
        Debug.Log("died");
        OnDied?.Invoke();
    }

    [ContextMenu("TakeLethalDamage")]
    private void TakeLethalDamage()
    {
        TakeHit(Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}