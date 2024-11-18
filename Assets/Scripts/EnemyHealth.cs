using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public event Action OnEnemyDeath; // Notify about death position
    public bool IsDead = false;
    
    protected override void Die()
    {
        base.Die();
        //OnEnemyDeath?.Invoke(transform.position); // Notify listeners with position
        OnEnemyDeath?.Invoke(); // Notify listeners with position
        IsDead = true;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage); // Call the base implementation
        
    }
    
    [ProButton]
    public void TestEnemyDamage()
    {
        Debug.Log("Enemy damage");
        TakeDamage(10);
    }
}