using UnityEngine;

public class MovingRectTransformPositionController : RectTransformPositionController
{
	[SerializeField] private Vector2 movementSpeed;

	private void Update()
	{
		AddPosition(movementSpeed*Time.deltaTime);
	}
}