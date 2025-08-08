using UnityEngine;

public class StageTimeManager : MonoBehaviour
{
	private StageStateManager stageStateManager;

	private void Awake()
	{
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();

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
			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		Time.timeScale = stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused) ? 0f : 1f;
	}
}