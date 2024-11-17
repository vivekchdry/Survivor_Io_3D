using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action OnPlayerJump; // Event for jump notifications

    public static void PlayerJump()
    {
        OnPlayerJump?.Invoke();
    }
}