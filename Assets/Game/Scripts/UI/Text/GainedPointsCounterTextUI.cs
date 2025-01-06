using UnityEngine;

[RequireComponent(typeof(RectTransformPositionController), typeof(IntCounter))]
public class GainedPointsCounterTextUI : MonoBehaviour
{
	private RectTransformPositionController rectTransformPositionController;
	private IntCounter intCounter;
	
	public void Setup(Vector2 position, int points)
	{
		rectTransformPositionController.SetPosition(position);
		intCounter.SetTo(points);
	}

	private void Awake()
	{
		rectTransformPositionController = GetComponent<RectTransformPositionController>();
		intCounter = GetComponent<IntCounter>();
	}
}