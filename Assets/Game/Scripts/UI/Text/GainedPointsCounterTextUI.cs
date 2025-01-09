using UnityEngine;

[RequireComponent(typeof(IntCounter), typeof(TimedRectTransformPositionController))]
public class GainedPointsCounterTextUI : MonoBehaviour
{
	[SerializeField] private Vector2 targetPositionOffset;
	
	private IntCounter intCounter;
	private TimedRectTransformPositionController timedRectTransformPositionController;
	
	public void Setup(int points, Vector2 position)
	{
		intCounter.SetTo(points);
		timedRectTransformPositionController.SetInitialPosition(position);
		timedRectTransformPositionController.SetTargetPosition(position + targetPositionOffset);
	}

	private void Awake()
	{
		intCounter = GetComponent<IntCounter>();
		timedRectTransformPositionController = GetComponent<TimedRectTransformPositionController>();
	}
}