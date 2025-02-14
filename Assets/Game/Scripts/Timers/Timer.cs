using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	public bool TimerWasStarted {get; private set;}
	public bool TimerWasFinished {get; private set;}
	
	public UnityEvent timerStartedEvent;
	public UnityEvent timerFinishedEvent;

	[SerializeField] private bool startImmediately = true;
	[SerializeField, Min(0f)] private float duration;

	private float currentTime;

	public float GetDuration() => duration;
	public float GetProgressPercent() => duration > 0 ? currentTime / duration : 0;

	public void StartTimerWithSetDuration(float duration)
	{
		SetDuration(duration);
		StartTimer();
	}

	public void StartTimer()
	{
		SetAsFinished(false);
		timerStartedEvent?.Invoke();
	}

	public void InterruptTimerIfPossible(bool invokeReachedEndEvent = false)
	{
		if(TimerWasStarted)
		{
			FinishTimer(invokeReachedEndEvent);
		}
	}

	public void SetDuration(float duration)
	{
		this.duration = duration;
	}

	private void Start()
	{
		if(startImmediately)
		{
			StartTimer();
		}
	}
	
	private void Update()
	{
		if(!TimerWasStarted)
		{
			return;
		}
		
		if(currentTime < duration)
		{
			currentTime = (currentTime + Time.deltaTime).GetClampedValue(0f, duration);
		}
		else if(!TimerWasFinished)
		{
			FinishTimer();
		}
	}

	private void FinishTimer(bool invokeReachedEndEvent = true)
	{
		SetAsFinished(true);

		if(invokeReachedEndEvent)
		{
			timerFinishedEvent?.Invoke();
		}
	}

	private void SetAsFinished(bool finished)
	{
		currentTime = finished ? duration : 0f;
		TimerWasStarted = !finished;
		TimerWasFinished = finished;
	}
}