using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[Min(0.01f)] public float duration;
	public bool countFromTheStart = true;
	public UnityEvent timerStartedEvent;
	public UnityEvent onReset, onEnd, onInterrupt;
	
	public bool Started {get; private set;}
	public bool Finished {get; private set;}

	private float timer;

	public void StartTimer()
	{
		if(!Started)
		{
			Started = true;

			timerStartedEvent?.Invoke();
		}
		else
		{
			ResetTimer();
		}
	}

	public void ResetTimer()
	{
		timer = 0f;
		
		SetAsFinished(false);
		onReset.Invoke();
	}

	public void InterruptTimer()
	{
		if(!Started)
		{
			return;
		}

		timer = duration;
		
		SetAsFinished(true);
		onInterrupt.Invoke();
	}

	public bool ReachedTheEnd() => timer >= duration;
	public float ProgressPercent() => timer / duration;

	private void Start() => StartTimerImmediately();
	private void Modify() => timer = Mathf.Clamp(timer + Time.deltaTime, 0f, duration);

	private void StartTimerImmediately()
	{
		if(countFromTheStart)
		{
			StartTimer();
		}
	}

	private void Update()
	{
		if(Started)
		{
			if(!ReachedTheEnd())
			{
				Modify();
			}
			else if(!Finished)
			{
				Finish();
			}
		}
	}
	
	private void Finish()
	{
		SetAsFinished(true);
		onEnd.Invoke();
	}

	private void SetAsFinished(bool finished)
	{
		Started = !finished;
		Finished = finished;
	}
}