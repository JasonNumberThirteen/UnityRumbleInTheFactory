using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotMovement : EntityMovement
{
	public LayerMask linecastDetectionLayers;
	[Min(0.01f)] public float linecastDetectionDistance = 0.5f;
	public Timer timer;
	public RobotCollisionDetector collisionDetector;

	private bool detectedCollision;
	private EnemyRobotFreeze freeze;
	private float lastMovementSpeed;
	private Vector2 lastDirection;

	public void SetMovementLock()
	{
		if(LastDirectionIsNotZero())
		{
			SetDirections(Direction, lastDirection);
		}
	}

	public void RandomiseDirection()
	{
		Vector2 direction = RandomDirection();

		SetDirections(direction, direction);
	}

	public void EnableCollisionDetection()
	{
		detectedCollision = false;
		movementSpeed = lastMovementSpeed;
	}

	protected override void Awake()
	{
		base.Awake();

		freeze = GetComponent<EnemyRobotFreeze>();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		DetectObstacles();
	}

	private void SetDirections(Vector2 newLastDirection, Vector2 newDirection)
	{
		if(freeze.Frozen)
		{
			lastDirection = newLastDirection;
		}
		else
		{
			SetDirection(newDirection);
		}
	}

	private void SetDirection(Vector2 direction)
	{
		Direction = direction;

		collisionDetector.AdjustRotation(Direction);
	}

	private void Start() => SetDirection(Vector2.down);
	private bool LastDirectionIsNotZero() => lastDirection != Vector2.zero;

	private Vector2 RandomDirection()
	{
		List<Vector2> directions = AvailableDirections();
		Vector2 randomDirection = RandomAvailableDirection(directions);

		if(directions.Count > 1 && randomDirection == Direction)
		{
			return RandomDirection();
		}

		return randomDirection;
	}

	private List<Vector2> AvailableDirections()
	{
		List<Vector2> directions = new List<Vector2>{Vector2.up, Vector2.down, Vector2.left, Vector2.right};
		Vector2 start = transform.position;

		directions.RemoveAll(e => Physics2D.Linecast(start, start + e*linecastDetectionDistance, linecastDetectionLayers));

		return directions;
	}

	private Vector2 RandomAvailableDirection(List<Vector2> directions)
	{
		int index = RandomAvailableDirectionIndex(directions);

		return directions[index];
	}

	private int RandomAvailableDirectionIndex(List<Vector2> directions) => Random.Range(0, directions.Count);
	
	private void DetectObstacles()
	{
		if(DetectedCollision())
		{
			DisableCollisionDetection();
		}
	}

	private bool DetectedCollision() => !detectedCollision && !freeze.Frozen && collisionDetector.OverlapBoxAll().Length > 1;

	private void DisableCollisionDetection()
	{
		timer.ResetTimer();

		detectedCollision = true;
		lastMovementSpeed = movementSpeed;
		movementSpeed = 0f;
	}

	private void OnDrawGizmos()
	{
		if(collisionDetector == null)
		{
			return;
		}
		
		Collider2D[] colliders = collisionDetector.OverlapBoxAll();

		foreach (Collider2D collider in colliders)
		{
			Gizmos.color = Color.red;
			
			Gizmos.DrawWireCube(collider.transform.position, collider.bounds.size);
		}

		Vector2[] directions = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

		foreach (Vector2 direction in directions)
		{
			Vector2 start = transform.position;
			Vector2 end = start + direction*linecastDetectionDistance;

			Gizmos.color = Color.white;

			if(Physics2D.Linecast(start, end, linecastDetectionLayers))
			{
				Gizmos.color = Color.red;
			}
			
			Gizmos.DrawLine(start, start + direction*linecastDetectionDistance);
		}
	}
}