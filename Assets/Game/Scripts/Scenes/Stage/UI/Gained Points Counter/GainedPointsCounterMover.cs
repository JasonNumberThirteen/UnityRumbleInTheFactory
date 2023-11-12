using UnityEngine;

public class GainedPointsCounterMover : MonoBehaviour
{
	[Min(0.01f)] public float movementSpeed = 1f;
	
	private RectTransformMover mover;

	private void Awake() => mover = GetComponent<RectTransformMover>();
	private void Update() => mover.AddPositionY(movementSpeed*Time.deltaTime);
}