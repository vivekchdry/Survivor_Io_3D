using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 10;
    public static event Action<int> OnCoinCollected;

    public void Collect()
    {
        OnCoinCollected?.Invoke(coinValue);
        Destroy(gameObject);
    }
}