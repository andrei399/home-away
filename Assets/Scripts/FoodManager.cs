using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private static FoodManager s_Instance;
    public static FoodManager Instance => s_Instance ??= FindFirstObjectByType<FoodManager>();

    private static List<GameObject> _meatCubes = new List<GameObject>();
    private static int _activeMeatCubeCount;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _meatCubes.Add(transform.GetChild(i).gameObject);
        };
        _activeMeatCubeCount = _meatCubes.Count;

        EatAnimationManager.OnEatingAnimationEnd += WinIfNoFoodRemains;
        EatAnimationManager.OnEatingAnimationEnter += TakeBite;
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
    }
}