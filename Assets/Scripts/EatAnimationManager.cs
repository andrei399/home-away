using System;
using UnityEngine;

public class EatAnimationManager : MonoBehaviour
{
    public static event Action<string> OnAnimationEnd;

    public void TriggerAnimationEnd(string animationName)
    {
        OnAnimationEnd?.Invoke(animationName);
    }
}