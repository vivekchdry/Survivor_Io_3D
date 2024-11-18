using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;

    public static event Action<int> OnHealthCollected; // Event to notify healing

    public void Collect()
    {
        Debug.Log("HealthPickup collected.");
        OnHealthCollected?.Invoke(healAmount);
        Destroy(gameObject); // Remove the pickup after collection
    }
}