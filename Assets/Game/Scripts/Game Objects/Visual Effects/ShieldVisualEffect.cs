using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ShieldVisualEffect : GameVisualEffect
{
	private Timer timer;
	
	private void Awake()
	{
		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.onReset.AddListener(OnTimerReset);
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onReset.RemoveListener(OnTimerReset);
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerReset()
	{
		SetActive(true);
	}

	private void OnTimerEnd()
	{
		SetActive(false);
	}
}