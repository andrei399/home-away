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

    public Transform easyPlate;
    public Transform mediumPlate;
    public Transform hardPlate;

    private Transform _selectedPlate;
    private Dictionary<DifficultyLevel, Transform> difficultyPlateMapper;

    private Coroutine _eatSoundEffect;
    private Animator _animator;

    private AudioSource audioSource;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SetPlates();
        
    }

    public void SetPlates()
    {
        EatAnimationManager.OnEatingAnimationEnd -= StopEating;
        EatAnimationManager.OnEatingAnimationEnter -= TakeBite;
        EatAnimationManager.OnAteFood -= PlayEatingSoundEffect;

        getPlate();
        
        _selectedPlate.gameObject.SetActive(true);
        

        _activeMeatCubeCount = _meatCubes.Count;

        EatAnimationManager.OnEatingAnimationEnd += StopEating;
        EatAnimationManager.OnEatingAnimationEnter += TakeBite;
        EatAnimationManager.OnAteFood += PlayEatingSoundEffect;
    }

    private Transform getPlate()
    {
        
        difficultyPlateMapper = new Dictionary<DifficultyLevel, Transform>{
            {DifficultyLevel.Easy, easyPlate},
            {DifficultyLevel.Medium, mediumPlate},
            {DifficultyLevel.Hard, hardPlate}
        };

        foreach (KeyValuePair<DifficultyLevel,Transform> difficulties in difficultyPlateMapper)
        {
            difficulties.Value.gameObject.SetActive(false);
        }
        
        _selectedPlate = difficultyPlateMapper[GameManager.Instance.selectedDifficulty];
        Debug.Log(_selectedPlate);
        return _selectedPlate;
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
        _meatCubes.Clear();
        for (int i = 0; i < _selectedPlate.childCount; i++)
        {
            var cube = _selectedPlate.GetChild(i).gameObject;
            _meatCubes.Add(_selectedPlate.GetChild(i).gameObject);
            cube.SetActive(true);
        }
        _activeMeatCubeCount = _meatCubes.Count;
        canEatAgain = true;
    }

    public void PlayEatingSoundEffect()
    {
        if (_eatSoundEffect != null)
        {
            StopCoroutine(_eatSoundEffect);
            _eatSoundEffect = null;
        }
        _eatSoundEffect = StartCoroutine(EatingSound());
    }

    private IEnumerator EatingSound()
    {
        audioSource.Play();
        yield return new WaitForEndOfFrame();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
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