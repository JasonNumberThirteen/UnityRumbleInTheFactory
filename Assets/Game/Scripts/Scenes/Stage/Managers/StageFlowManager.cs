using UnityEngine;

[RequireComponent(typeof(Timer))]
public class StageFlowManager : MonoBehaviour
{
	private Timer timer;
	private StageStateManager stageStateManager;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();

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
		if(stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Interrupted))
		{
			StageManager.instance.SetGameAsOver();
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Interrupted)
		{
			timer.StartTimer();
		}
	}
}