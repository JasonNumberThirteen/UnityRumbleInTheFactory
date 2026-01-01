using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TimedRectTransformPositionController))]
public class GameOverTextUI : TextUI
{
	public UnityEvent targetPositionWasReachedEvent;
	
	private TimedRectTransformPositionController timedRectTransformPositionController;
	private StageStateManager stageStateManager;

	protected override void Awake()
	{
		base.Awake();

		timedRectTransformPositionController = GetComponent<TimedRectTransformPositionController>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		text.enabled = false;

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
			timedRectTransformPositionController.targetPositionWasReachedEvent.AddListener(OnTargetPositionWasReached);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.AddListener(OnStageStateWasChanged);
			}
		}
		else
		{
			timedRectTransformPositionController.targetPositionWasReachedEvent.RemoveListener(OnTargetPositionWasReached);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.RemoveListener(OnStageStateWasChanged);
			}
		}
	}

	private void OnTargetPositionWasReached()
	{
		targetPositionWasReachedEvent?.Invoke();
	}

	private void OnStageStateWasChanged(StageState stageState)
	{
		if(stageState != StageState.Over)
		{
			return;
		}

		text.enabled = true;
		
		timedRectTransformPositionController.StartTranslation();
	}
}