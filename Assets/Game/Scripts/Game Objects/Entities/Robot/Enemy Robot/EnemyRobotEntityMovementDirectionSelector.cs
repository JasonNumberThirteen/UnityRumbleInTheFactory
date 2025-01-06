using Random = UnityEngine.Random;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyRobotEntityMovementDirectionSelector : MonoBehaviour
{
	[SerializeField] private LayerMask obstacleDetectionLayerMask;
	[SerializeField, Min(0.01f)] private float obstacleDetectionDistance = 0.5f;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color availableDirectionGizmosColor = Color.white;
	[SerializeField] private Color unavailableDirectionGizmosColor = Color.red;

	private readonly List<Vector2> allDirections = new()
	{
		Vector2.up,
		Vector2.down,
		Vector2.left,
		Vector2.right
	};

	public Vector2 GetRandomDirection(Vector2 currentDirection)
	{
		var availableDirections = GetAvailableDirections();
		var randomDirection = GetRandomAvailableDirection(availableDirections);

		if(availableDirections.Count > 1 && randomDirection == currentDirection)
		{
			return GetRandomDirection(currentDirection);
		}

		return randomDirection;
	}

	private Vector2 GetRandomAvailableDirection(List<Vector2> availableDirections)
	{
		if(availableDirections == null || availableDirections.Count == 0)
		{
			return Vector2.zero;
		}
		
		var randomIndex = Random.Range(0, availableDirections.Count);

		return availableDirections[randomIndex];
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos)
		{
			DrawAvailableDirections();
		}
	}

	private void DrawAvailableDirections()
	{
		var directions = GetAllDirections();

		foreach (var direction in directions)
		{
			var start = transform.position;
			var end = GetLinecastEnd(start, direction);
			var color = Linecast(start, direction) ? unavailableDirectionGizmosColor : availableDirectionGizmosColor;
			
			GizmosMethods.OperateOnGizmos(() => Gizmos.DrawLine(start, end), color);
		}
	}

	private List<Vector2> GetAvailableDirections() => GetAllDirections().Where(vector => !Linecast(transform.position, vector)).ToList();
	private List<Vector2> GetAllDirections() => allDirections;
	private RaycastHit2D Linecast(Vector2 start, Vector2 direction) => Physics2D.Linecast(start, GetLinecastEnd(start, direction), obstacleDetectionLayerMask);
	private Vector2 GetLinecastEnd(Vector2 start, Vector2 direction) => start + direction*obstacleDetectionDistance;
}