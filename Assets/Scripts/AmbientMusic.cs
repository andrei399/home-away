using System.Collections;
using UnityEngine;

public class AmbientMusic : MonoBehaviour
{
	private static AmbientMusic s_Instance;
	public static AmbientMusic Instance => s_Instance ??= FindFirstObjectByType<AmbientMusic>();
	[SerializeField]
	private AudioClip[] _clips;

	private AudioSource _audioSource;

	private Coroutine _playAmbientCoroutine;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		StartCoroutine(PlayAudioSequentially());
	}

	private IEnumerator PlayAudioSequentially()
	{
		if (_clips == null || _clips.Length == 0)
		{
			yield break;
		}
		int i = 0;

		while (true)
		{
			_audioSource.clip = _clips[i];
			_audioSource.Play();

			yield return new WaitWhile(() => _audioSource.isPlaying);

			i = (i + 1) % _clips.Length;
		}
	}

}