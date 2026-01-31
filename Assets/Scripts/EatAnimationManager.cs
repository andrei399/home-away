using System;
using UnityEngine;

public class EatAnimationManager : MonoBehaviour
{
    public static event Action OnEatingAnimationEnter;
    public static event Action OnEatingAnimationEnd;

    public void TriggerAnimationEnd()
    {
        OnEatingAnimationEnd?.Invoke();
    }

    public void TriggerAnimationEnter()
    {
        OnEatingAnimationEnter?.Invoke();
    }
}