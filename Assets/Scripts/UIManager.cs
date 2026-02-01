using DefaultNamespace;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager s_Instance;
	public static UIManager Instance => s_Instance ??= FindFirstObjectByType<UIManager>();

	private Animator _uiAnimator;
	
	[SerializeField]
	private CovidProgressBar _covidProgressBar;

	[SerializeField]
	private FoodVirusProgressBar _foodVirusProgressBar;

	[SerializeField]
	private GameObject _mainMenu;

	[SerializeField]
	private TMP_Text _endScreenTitle;

	[SerializeField]
	private TMP_Text _endScreenBody;

	[SerializeField]
	private Slider _sfxSlider;

	[SerializeField]
	private Slider _musicSlider;
	public Slider MusicSlider => _musicSlider;

	[field:SerializeField]
	public TMP_Dropdown DifficultyDropdown { get; private set; }

	private bool _won;

    public ParticleSystem stinkyParticles;

	private void Awake()
	{
		_covidProgressBar.OnProgressBarFilled += TriggerGameOver;
		_foodVirusProgressBar.OnProgressBarFilled += TriggerGameOver;
		_uiAnimator = GetComponent<Animator>();
		ChangeProgressBarStates(false);

		_sfxSlider.onValueChanged.AddListener(SetSFXVolume);
	}

	private void OnDestroy()
	{
		_covidProgressBar.OnProgressBarFilled -= TriggerGameOver;
		_foodVirusProgressBar.OnProgressBarFilled -= TriggerGameOver;
		
		_sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
	}

	private void TriggerGameOver()
	{
		GameManager.Instance.GameOver();
		_endScreenTitle.SetText("Unfortunate :( you got the damn virus.");
		_endScreenBody.SetText("Your abuelita is going to miss you while you're in quarantine.\n Pay 50 robux for quick heal and try again?");
		_uiAnimator.Play("GameOverFadeIn");
		Debug.Log("GAME OVER!");
		ChangeProgressBarStates(false);
	}

	public void IncreaseCovidAmount()
	{
		_covidProgressBar.IncreaseBar(0.25f);
	}

	public void IncreaseFoodVirusAmount()
	{
		_foodVirusProgressBar.IncreaseBar(0.1f);
		var maxParticles = 20; // should match what we have in inspector
		var emissions = stinkyParticles.emission;
        emissions.rateOverTime = maxParticles * _foodVirusProgressBar.ProgressBarImage.fillAmount;
	}
	
	public void StartGame()
	{
		_mainMenu.SetActive(false);
		GameManager.Instance.Retry();
		StartCoroutine(AmbientMusic.Instance.PlayAudioSequentially());
		ChangeProgressBarStates(true);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void RetryPressed()
	{
		Debug.Log("Retry");	
		_covidProgressBar.ResetBar();
		_foodVirusProgressBar.ResetBar();
		_uiAnimator.Play("GameOverFadeOut");
		GameManager.Instance.Retry();
		ChangeProgressBarStates(true);
	}

	public void TriggerGameWon()
	{
		_endScreenTitle.SetText("Congratulations! You finished your meal.");
		_endScreenBody.SetText("You can now go back to your abuelita and keep her safe <3 ! \n Try another meal in these hard times?");
		_uiAnimator.Play("GameOverFadeIn");
		ChangeProgressBarStates(false);
	}

	private void SetSFXVolume(float value)
	{
		GameManager.Instance.SetSFXVolume(value);
	}

	private void ChangeProgressBarStates(bool enabled)
	{
		_covidProgressBar.gameObject.SetActive(enabled);
		_foodVirusProgressBar.gameObject.SetActive(enabled);
		var emissions = stinkyParticles.emission;
        emissions.rateOverTime = 0;
	}
}