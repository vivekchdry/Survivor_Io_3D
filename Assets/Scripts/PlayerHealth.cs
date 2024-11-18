using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public static event Action<int, int> OnHealthChanged; // Notify health change
    public static event Action OnPlayerDeath; // Notify systems of player death
    
    protected override void Die()
    {
        base.Die();
        OnPlayerDeath?.Invoke(); // Notify listeners
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage); // Call the base implementation
        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify health change
    }

    public override void Heal(int amount)
    {
        base.Heal(amount); // Call the base implementation
        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify health change
    }

    [ProButton]
    public void TestPlayerDamage()
    {
        Debug.Log("Player damage");
        TakeDamage(10);
    }
}