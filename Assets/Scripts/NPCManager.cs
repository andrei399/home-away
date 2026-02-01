using UnityEngine;

public class NPCManager : MonoBehaviour
{
	private static NPCManager s_Instance;
	public static NPCManager Instance => s_Instance ??= FindFirstObjectByType<NPCManager>();
	[SerializeField]
	private Animator[] _npcControllers;

	public void CoughAnimation()
	{
		int randomNpc = Random.Range(0, _npcControllers.Length);
		_npcControllers[randomNpc].SetTrigger("Cough");
	}
}