using UnityEngine;

namespace Data
{
	[CreateAssetMenu(menuName = "SoundData", fileName = "SoundData")]
	public class SoundData : ScriptableObject
	{
		[field: SerializeField]
		public Sound[] Sounds;
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
	Cough,
	NotHarmful
}