using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    
    private AudioSource _audioSource;
    private SoundsSpawner _soundsSpawner;
    private float _soundSpawnInterval;
    private bool _isMaskOn;

    private float _putMaskOnInterval;
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
        _timer += Time.deltaTime;

        if (_timer >= _soundSpawnInterval)
        {
            _timer = 0f;
            _soundSpawnInterval = Random.Range(2f, 6f);
            if (_soundsSpawner.PlayRandomSound())
            {
                Debug.Log("GAMEMANAGER - MASK ON BOY");
                StartCoroutine(TimeToPutMaskOn());
            } 
            else
            {
                Debug.Log("GAMEMANAGER - MASK OFF BOY");
            }
        }
        
    }
    
    private void HandleMaskState(bool isMaskOn)
    {
        _isMaskOn = isMaskOn;
    }

    private IEnumerator TimeToPutMaskOn()
    {
        yield return new WaitForSeconds(_putMaskOnInterval);
        if (_isMaskOn)
        {
            Debug.Log("Mask was on, nice job!");
        } 
        else
        {
            Debug.Log("Mask was off, covid meter increased");
        }
    }
}