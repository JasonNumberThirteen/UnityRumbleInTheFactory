using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TimedRectTransformPositionController))]
public class GameOverTextUI : TextUI
{
	public UnityEvent textReachedTargetPositionEvent;
	
	private TimedRectTransformPositionController timedRectTransformPositionController;

	protected override void Awake()
	{
		base.Awake();

		timedRectTransformPositionController = GetComponent<TimedRectTransformPositionController>();

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
		}
		else
		{
			timedRectTransformPositionController.rectTransformReachedTargetPositionEvent.RemoveListener(OnRectTransformReachedTargetPosition);
		}
	}

	private void OnRectTransformReachedTargetPosition()
	{
		textReachedTargetPositionEvent?.Invoke();
	}
}