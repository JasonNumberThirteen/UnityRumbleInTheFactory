using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ShieldGameVisualEffect : GameVisualEffect
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
			timer.timerWasResetEvent.AddListener(OnTimerWasReset);
			timer.timerReachedEndEvent.AddListener(OnTimerReachedEnd);
		}
		else
		{
			timer.timerWasResetEvent.RemoveListener(OnTimerWasReset);
			timer.timerReachedEndEvent.RemoveListener(OnTimerReachedEnd);
		}
	}

	private void OnTimerWasReset()
	{
		SetActive(true);
	}

	private void OnTimerReachedEnd()
	{
		SetActive(false);
	}
}