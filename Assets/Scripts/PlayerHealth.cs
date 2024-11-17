using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public static event Action OnPlayerDeath; // Notify systems of player death

    protected override void Die()
    {
        base.Die();
        OnPlayerDeath?.Invoke(); // Notify listeners
    }

    [ProButton]
    public void TestPlayerDamage()
    {
        Debug.Log("Player damage");
        TakeDamage(10);
    }
}