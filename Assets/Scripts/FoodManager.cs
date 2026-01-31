using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private static FoodManager s_Instance;
    public static FoodManager Instance => s_Instance ??= FindFirstObjectByType<FoodManager>();

    private static List<GameObject> _meatCubes = new List<GameObject>();
    private static int _activeMeatCubeCount;

    public bool isEating = false;
    public bool canEatAgain = true;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _meatCubes.Add(transform.GetChild(i).gameObject);
        };
        _activeMeatCubeCount = _meatCubes.Count;

        EatAnimationManager.OnEatingAnimationEnd += StopEating;
        EatAnimationManager.OnEatingAnimationEnter += TakeBite;
        EatAnimationManager.OnAteFood += PlayEatingSoundEffect;
    }

    public void WinIfNoFoodRemains()
    {
        Debug.Log("Winning");
        if (_activeMeatCubeCount == 0)
        {
            GameManager.Instance.WinGame();
        }
    }

    public void TakeBite()
    {
        Debug.Log("TakingBite");
        _meatCubes[^_activeMeatCubeCount].SetActive(false);
        _activeMeatCubeCount -= 1;
    }

    public void ResetFood()
    {
        Debug.Log("Resetting Food");
        foreach(var meatcube in _meatCubes)
        {
            meatcube.SetActive(true);
        }
        _activeMeatCubeCount = _meatCubes.Count;
        canEatAgain = true; 
    }

    public void PlayEatingSoundEffect()
    {
        StartCoroutine(EatingSound());
    }

    private IEnumerator EatingSound()
    {
        var source = GameManager.Instance.PlayEatingSound();
        while (source.isPlaying)
        {
            yield return null;
        };
        canEatAgain = true;
        Debug.Log("Can Eat Again");
    }

    public void Eat()
    {
        Debug.Log("StartEating");
        isEating = true;
        canEatAgain = false;
        _animator.Play("EatFull", 0, 0f);
    }

    private void StopEating()
    {
        Debug.Log("StopEating");
        WinIfNoFoodRemains();
        isEating = false;
    }
}