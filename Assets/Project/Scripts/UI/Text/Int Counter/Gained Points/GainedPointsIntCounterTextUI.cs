using UnityEngine;

[RequireComponent(typeof(TimedRectTransformPositionController))]
public class GainedPointsIntCounterTextUI : IntCounterTextUI
{
	[SerializeField] private Vector2 targetPositionOffset;
	
	private TimedRectTransformPositionController timedRectTransformPositionController;
	
	public void SetPosition(Vector2 position)
	{
		timedRectTransformPositionController.SetInitialPosition(position);
		timedRectTransformPositionController.SetTargetPosition(position + targetPositionOffset);
	}

	protected override void Awake()
	{
		base.Awake();

		timedRectTransformPositionController = GetComponent<TimedRectTransformPositionController>();
	}
}