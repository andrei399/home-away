using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
}

public class GameManager : MonoBehaviour
{
    private static GameManager s_Instance;
    public static GameManager Instance => s_Instance ??= FindFirstObjectByType<GameManager>();

    private bool _gameRunning = false;
    public bool GameRunning => _gameRunning;
    
    [SerializeField]
    private Player _player;
    
    private AudioSource _audioSource;
    private SoundsSpawner _soundsSpawner;
    private float _soundSpawnInterval;
    private bool _isMaskOn;

    private float _putMaskOnInterval;

    public DifficultyLevel selectedDifficulty = DifficultyLevel.Easy;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _soundsSpawner = new SoundsSpawner(_audioSource);
        _soundSpawnInterval = Random.Range(2f, 6f);
        _player.MaskState += HandleMaskState;
        _putMaskOnInterval = 1f;
    }

    private float _timer = 0f;
    private void Update()
    {
        if (!_gameRunning)
        {
            return;
        }
        _timer += Time.deltaTime;

        if (_timer >= _soundSpawnInterval)
        {
            _timer = 0f;
            _soundSpawnInterval = Random.Range(2f, 6f);
            if (_soundsSpawner.PlayRandomSound())
            {
                _audioSource.volume = 1f;
                NPCManager.Instance.CoughAnimation();
                StartCoroutine(TimeToPutMaskOn());
            } 
            else
            {
                _audioSource.volume = 0.25f;
            }
        }
        
    }

    public void GameOver()
    {
        Debug.Log("PROGRESS BAR FILLED");
        _gameRunning = false;
    }

    public void WinGame()
    {
        Debug.Log("WIN GAME :PARTY:");
        _gameRunning = false;
        UIManager.Instance.TriggerGameWon();
    }

    public void Retry()
    {
        _gameRunning = true;
        _soundSpawnInterval = Random.Range(2f, 6f);
        FoodManager.Instance.ResetFood();
        _timer = 0;
    }
    
    private void HandleMaskState(bool isMaskOn)
    {
        _isMaskOn = isMaskOn;
    }

    private IEnumerator TimeToPutMaskOn()
    {
        yield return new WaitForSeconds(_putMaskOnInterval);
        UIManager.Instance.IncreaseFoodVirusAmount();
        if (_isMaskOn)
        {
            Debug.Log("Mask was on, nice job!");
        } 
        else
        {
            Debug.Log("Mask was off, covid meter increased");
            UIManager.Instance.IncreaseCovidAmount();
        }
    }

    public AudioSource PlayEatingSound()
    {
        return _soundsSpawner.PlaySound(_soundsSpawner.SoundData.EatingSound);
    }
}