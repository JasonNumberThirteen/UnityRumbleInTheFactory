using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TimedRectTransformPositionController))]
public class GameOverTextUI : TextUI
{
	public UnityEvent textReachedTargetPositionEvent;
	
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
			timedRectTransformPositionController.rectTransformReachedTargetPositionEvent.AddListener(OnRectTransformReachedTargetPosition);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			timedRectTransformPositionController.rectTransformReachedTargetPositionEvent.RemoveListener(OnRectTransformReachedTargetPosition);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnRectTransformReachedTargetPosition()
	{
		textReachedTargetPositionEvent?.Invoke();
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState != StageState.Over)
		{
			return;
		}

		text.enabled = true;
		
		timedRectTransformPositionController.StartTranslation();
	}
}