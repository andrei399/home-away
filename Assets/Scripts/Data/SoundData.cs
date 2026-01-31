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