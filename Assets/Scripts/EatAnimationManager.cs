using System;
using UnityEngine;

public class EatAnimationManager : MonoBehaviour
{
    public static event Action OnEatingAnimationEnd;

    public void TriggerAnimationEnd()
    {
        OnEatingAnimationEnd?.Invoke();
    }
}