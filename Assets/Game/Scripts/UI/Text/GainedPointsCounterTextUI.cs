using UnityEngine;

[RequireComponent(typeof(IntCounter), typeof(RectTransformPositionController))]
public class GainedPointsCounterTextUI : MonoBehaviour
{
	private IntCounter intCounter;
	private RectTransformPositionController rectTransformPositionController;
	
	public void Setup(int points, Vector2 position)
	{
		intCounter.SetTo(points);
		rectTransformPositionController.SetPosition(position);
	}

	private void Awake()
	{
		intCounter = GetComponent<IntCounter>();
		rectTransformPositionController = GetComponent<RectTransformPositionController>();
	}
}