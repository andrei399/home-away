using UnityEngine;

namespace Data
{
	[CreateAssetMenu(menuName = "SoundData", fileName = "SoundData")]
	public class SoundData : ScriptableObject
	{
		[field: SerializeField]
		public Sound[] CoughSounds;
		
		[field: SerializeField]
		public Sound[] RandomSounds;

		[field: SerializeField]
		public AudioClip EatingSound;

		[field: SerializeField]
		public AudioClip BackgroundSound;
	}
}

[System.Serializable]
public struct Sound
{
	public SoundType SoundType;
	public AudioClip SoundClip;
}

public enum SoundType
{
	Harmful,
	NotHarmful
}