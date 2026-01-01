using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotEntityMovementDirectionSelector : MonoBehaviour
{
	[SerializeField] private LayerMask detectableGameObjects;
	[SerializeField, Min(0.01f)] private float obstacleDetectionDistance = 0.5f;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color availableDirectionGizmosColor = Color.white;
	[SerializeField] private Color unavailableDirectionGizmosColor = Color.red;

	private readonly List<Vector2> allDirections = new() {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

	public Vector2 GetRandomDirection(Vector2 currentDirection)
	{
		var availableDirections = GetAvailableDirections();
		var randomDirection = availableDirections.GetRandomElement();

		if(availableDirections.Count > 1 && randomDirection == currentDirection)
		{
			return GetRandomDirection(currentDirection);
		}

		return randomDirection;
	}

	private void OnDrawGizmos()
	{
		if(!drawGizmos)
		{
			return;
		}

		allDirections.ForEach(direction =>
		{
			var start = transform.position;
			var end = GetLinecastEnd(start, direction);
			var color = Linecast(start, direction) ? unavailableDirectionGizmosColor : availableDirectionGizmosColor;
			
			GizmosMethods.OperateOnGizmos(() => Gizmos.DrawLine(start, end), color);
		});
	}

	private List<Vector2> GetAvailableDirections() => allDirections.Where(vector => !Linecast(transform.position, vector)).ToList();
	private RaycastHit2D Linecast(Vector2 start, Vector2 direction) => Physics2D.Linecast(start, GetLinecastEnd(start, direction), detectableGameObjects);
	private Vector2 GetLinecastEnd(Vector2 start, Vector2 direction) => start + direction*obstacleDetectionDistance;
}