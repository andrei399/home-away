using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static List<GameObject> meatCubes = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            meatCubes.Append(transform.GetChild(i).gameObject);
        };

        EatAnimationManager.OnEatingAnimationEnd += WinIfNoFoodRemains;
    }

    public void WinIfNoFoodRemains()
    {
        if (meatCubes.Count == 0)
        {
            // Win thingy.
        }
    }

    public void TakeBite()
    {
        meatCubes.RemoveAt(0);
    }
}