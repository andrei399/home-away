using System;
using System.Collections;
using UnityEngine;

public class AmbientMusic : MonoBehaviour
{
	private static AmbientMusic s_Instance;
	public static AmbientMusic Instance => s_Instance ??= FindFirstObjectByType<AmbientMusic>();
	[SerializeField]
	private AudioClip[] _clips;

	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public IEnumerator PlayAudioSequentially()
	{
		yield return null;

		for (int i = 0; i < _clips.Length; i++)
		{
			_audioSource.clip = _clips[i];

			_audioSource.Play();

			while (_audioSource.isPlaying)
			{
				yield return null;
			}
		}
	}

}