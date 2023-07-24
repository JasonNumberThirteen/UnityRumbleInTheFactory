using UnityEngine;

public class GainedPointsCounterMover : MonoBehaviour
{
	[Min(0.01f)] public float movementSpeed = 1f;
	
	private RectTransform rectTransform;
	private RectTransformMover mover;
	private Timer timer;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		mover = GetComponent<RectTransformMover>();
		timer = GetComponent<Timer>();
	}

	private void Update() => mover.AddPositionY(movementSpeed*Time.deltaTime);
}