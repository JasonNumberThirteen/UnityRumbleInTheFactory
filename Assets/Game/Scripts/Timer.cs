using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[Min(0.01f)] public float duration;
	public bool countFromTheStart = true;
	public UnityEvent onReset, onEnd;
	
	public bool Started {get; private set;}
	public bool Finished {get; private set;}

	private float timer;

	public void StartTimer()
	{
		if(!Started)
		{
			Started = true;
		}
		else
		{
			ResetTimer();
		}
	}

	public void ResetTimer()
	{
		Started = true;
		Finished = false;
		timer = 0f;

		onReset.Invoke();
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
		Started = false;
		Finished = true;

		onEnd.Invoke();
	}
}