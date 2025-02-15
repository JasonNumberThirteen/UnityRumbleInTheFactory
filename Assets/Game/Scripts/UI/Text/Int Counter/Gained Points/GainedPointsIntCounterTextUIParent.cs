using UnityEngine;

[RequireComponent(typeof(IntCounter), typeof(GainedPointsIntCounterTextUI))]
public class GainedPointsIntCounterTextUIParent : MonoBehaviour
{
	private IntCounter intCounter;
	private GainedPointsIntCounterTextUI gainedPointsIntCounterTextUI;

	public void Setup(int points, Vector2 position)
	{
		intCounter.SetTo(points);
		gainedPointsIntCounterTextUI.SetPosition(position);
	}

	private void Awake()
	{
		intCounter = GetComponent<IntCounter>();
		gainedPointsIntCounterTextUI = GetComponent<GainedPointsIntCounterTextUI>();	
	}
}