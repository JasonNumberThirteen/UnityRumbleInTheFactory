using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[Min(0.01f)] public float duration;
	public bool countFromTheStart = true;
	public UnityEvent onEnd;

	private float timer;
	private bool started, finished;

	public void StartTimer() => started = true;
	public bool ReachedTheEnd() => timer >= duration;
	public float ProgressPercent() => timer / duration;

	private void Start()
	{
		if(countFromTheStart)
		{
			StartTimer();
		}
	}

	private void Update()
	{
		if(started)
		{
			if(!ReachedTheEnd())
			{
				Modify();
			}
			else if(!finished)
			{
				onEnd.Invoke();

				finished = true;
			}
		}
	}

	private void Modify() => timer = Mathf.Clamp(timer + Time.deltaTime, 0f, duration);
}