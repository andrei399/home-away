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
		int randomSound = Random.Range(0, SoundData.Sounds.Length);
		Sound sound = SoundData.Sounds[randomSound];
        _audioSource.PlayOneShot(sound.SoundClip);
        return sound.SoundType == SoundType.Cough;
	}
}