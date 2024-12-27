using UnityEngine;

public class MovingRectTransformPositionController : MonoBehaviour
{
	[Min(0.01f)] public float movementSpeed = 1f;
	
	private RectTransformPositionController mover;

	private void Awake() => mover = GetComponent<RectTransformPositionController>();
	private void Update() => mover.AddPositionY(movementSpeed*Time.deltaTime);
}