using UnityEngine;

public class StageTimeManager : MonoBehaviour
{
	private StageStateManager stageStateManager;

	private void Awake()
	{
		stageStateManager = FindAnyObjectByType<StageStateManager>(FindObjectsInactive.Include);

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
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		Time.timeScale = stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused) ? 0f : 1f;
	}
}