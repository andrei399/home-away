using DefaultNamespace;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager s_Instance;
	public static UIManager Instance => s_Instance ??= FindFirstObjectByType<UIManager>();

	private Animator _uiAnimator;
	
	[SerializeField]
	private CovidProgressBar _covidProgressBar;

	[SerializeField]
	private FoodVirusProgressBar _foodVirusProgressBar;

	private void Awake()
	{
		_covidProgressBar.OnProgressBarFilled += TriggerGameOver;
		_foodVirusProgressBar.OnProgressBarFilled += TriggerGameOver;
		_uiAnimator = GetComponent<Animator>();
	}

	private void OnDestroy()
	{
		_covidProgressBar.OnProgressBarFilled -= TriggerGameOver;
		_foodVirusProgressBar.OnProgressBarFilled -= TriggerGameOver;
	}

	private void TriggerGameOver()
	{
		GameManager.Instance.GameOver();
		_uiAnimator.Play("GameOverFadeIn");
		Debug.Log("GAME OVER!");
	}

	public void IncreaseCovidAmount()
	{
		_covidProgressBar.IncreaseBar(0.7f);
	}

	public void IncreaseFoodVirusAmount()
	{
		_foodVirusProgressBar.IncreaseBar(0.7f);
	}

	public void RetryPressed()
	{
		Debug.Log("Retry");	
		_covidProgressBar.ResetBar();
		_foodVirusProgressBar.ResetBar();
		_uiAnimator.Play("GameOverFadeOut");
		GameManager.Instance.Retry();
	}
}