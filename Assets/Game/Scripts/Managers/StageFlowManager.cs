using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class StageFlowManager : MonoBehaviour
{
	public UnityEvent stageStartedEvent;
	
	[SerializeField] private float delayOnStart = 1.5f;
	[SerializeField] private float delayAfterInterrupting = 1f;
	
	private Timer timer;
	private StageStateManager stageStateManager;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		timer.duration = delayOnStart;

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
			timer.onEnd.AddListener(OnTimerEnd);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnTimerEnd()
	{
		if(stageStateManager == null)
		{
			return;
		}

		if(stageStateManager.StateIsSetTo(StageState.Active))
		{
			stageStartedEvent?.Invoke();
		}
		else if(stageStateManager.StateIsSetTo(StageState.Interrupted))
		{
			StageManager.instance.SetGameAsOver();
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Interrupted)
		{
			timer.duration = delayAfterInterrupting;
			
			timer.ResetTimer();
		}
	}
}