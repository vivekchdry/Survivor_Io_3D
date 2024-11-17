using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public event Action<Vector3> OnEnemyDeath; // Notify about death position

    protected override void Die()
    {
        base.Die();
        OnEnemyDeath?.Invoke(transform.position); // Notify listeners with position
    }
    
    [ProButton]
    public void TestEnemyDamage()
    {
        Debug.Log("Enemy damage");
        TakeDamage(10);
    }
}