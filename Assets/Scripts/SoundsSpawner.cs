using Data;
using UnityEngine;

public class SoundsSpawner
{
	public SoundData SoundData { get; private set; }
	
	private AudioSource _audioSource;

	public SoundsSpawner(AudioSource source)
	{
		_audioSource = source;
		SoundData = Resources.Load<SoundData>("SoundData");
	}

	public bool PlayRandomSound()
	{
		bool useCoughSounds = Random.Range(0, 2) == 0;
		Sound[] chosenArray = useCoughSounds ? SoundData.CoughSounds : SoundData.RandomSounds;
		
		int index = Random.Range(0, chosenArray.Length);
        _audioSource.PlayOneShot(chosenArray[index].SoundClip);
        return chosenArray[index].SoundType == SoundType.Harmful;
	}
}