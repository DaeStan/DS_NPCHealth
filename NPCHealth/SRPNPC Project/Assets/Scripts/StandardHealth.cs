using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;

    private int currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHealth -= amount;

        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();
    }

    public void TakeHealth(int amount)
    {
        currentHealth += amount;

        OnHPPctChanged(CurrentHpPct);
    }


    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}
