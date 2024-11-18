using System;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    public int MaxHealth => maxHealth; // Public getter for max health
    public int CurrentHealth => currentHealth; // Public getter for current health

    public event Action<int, int> OnHealthChanged; // Current health, Max health
    public event Action OnDeath; // Entity death event

    protected virtual void Awake()
    {
        currentHealth = maxHealth; // Initialize health
        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Trigger initial health change
    }


    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
    }
}