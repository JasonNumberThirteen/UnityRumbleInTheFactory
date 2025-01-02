using Random = UnityEngine.Random;
using UnityEngine;
using System.Collections.Generic;

public class EnemyRobotMovementDirectionSelector : MonoBehaviour
{
	public LayerMask obstacleDetectionLayers;
	[Min(0.01f)] public float obstacleDetectionDistance = 0.5f;

	public Vector2 RandomDirection(Vector2 currentDirection)
	{
		List<Vector2> directions = AvailableDirections();
		Vector2 randomDirection = RandomAvailableDirection(directions);

		if(directions.Count > 1 && randomDirection == currentDirection)
		{
			return RandomDirection(currentDirection);
		}

		return randomDirection;
	}

	public List<Vector2> AllDirections() => new List<Vector2>{Vector2.up, Vector2.down, Vector2.left, Vector2.right};
	public RaycastHit2D Linecast(Vector2 start, Vector2 direction) => Physics2D.Linecast(start, LinecastEnd(start, direction), obstacleDetectionLayers);
	public Vector2 LinecastEnd(Vector2 start, Vector2 direction) => start + direction*obstacleDetectionDistance;

	private List<Vector2> AvailableDirections()
	{
		List<Vector2> directions = AllDirections();

		directions.RemoveAll(e => Linecast(transform.position, e));

		return directions;
	}

	private Vector2 RandomAvailableDirection(List<Vector2> directions)
	{
		int index = RandomAvailableDirectionIndex(directions);

		return directions[index];
	}

	private int RandomAvailableDirectionIndex(List<Vector2> directions) => Random.Range(0, directions.Count);
}