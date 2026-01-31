using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BaseProgressBar : MonoBehaviour
{
	[SerializeField]
	private Image _progressBarImage;

	public event Action OnProgressBarFilled; 

	private void Awake()
	{
		_progressBarImage.fillAmount = 0f;
	}

	public void IncreaseBar(float amount)
	{
		StartCoroutine(AnimateFill(amount, 0.5f));
	}

	public void ResetBar()
	{
		_progressBarImage.fillAmount = 0f;
	}

	private IEnumerator AnimateFill(float amount, float duration)
	{
		float elapsed = 0f;
		float start = _progressBarImage.fillAmount;
		float end = start + amount;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			_progressBarImage.fillAmount = Mathf.Lerp(start, end, elapsed / duration);
			yield return null;
		}

		_progressBarImage.fillAmount = end;
		if (Mathf.Approximately(_progressBarImage.fillAmount, 1f))
		{
			OnProgressBarFilled?.Invoke();
		}
	}
}