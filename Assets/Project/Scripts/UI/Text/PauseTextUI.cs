using System.Collections;
using UnityEngine;

public class PauseTextUI : TextUI
{
	[SerializeField, Min(0.01f)] private float blinkDelay = 0.5f;

	private IEnumerator coroutine;

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	protected override void Awake()
	{
		base.Awake();

		coroutine = SwitchSettingTextEnabled();
	}

	private void OnEnable()
	{
		StartCoroutine(coroutine);
	}

	private void OnDisable()
	{
		StopCoroutine(coroutine);
	}

	private IEnumerator SwitchSettingTextEnabled()
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(blinkDelay);

			text.enabled = !text.enabled;
		}
	}
}