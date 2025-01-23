using UnityEngine;

[RequireComponent(typeof(Timer))]
public class GameObjectTimedDestroyer : MonoBehaviour
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
			timer.timerReachedEndEvent.AddListener(OnTimerReachedEnd);
		}
		else
		{
			timer.timerReachedEndEvent.RemoveListener(OnTimerReachedEnd);
		}
	}

	private void OnTimerReachedEnd()
	{
		Destroy(gameObject);
	}
}