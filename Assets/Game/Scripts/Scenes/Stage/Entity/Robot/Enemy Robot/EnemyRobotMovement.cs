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

	public void Freeze()
	{
		if(LastDirectionIsNotZero())
		{
			lastDirection = Direction;
		}
	}

	public void Unfreeze()
	{
		if(LastDirectionIsNotZero())
		{
			SetDirection(lastDirection);
		}
	}

	public void RandomiseDirection()
	{
		Vector2 direction = RandomDirection();

		if(freeze.Frozen)
		{
			lastDirection = direction;
		}
		else
		{
			SetDirection(direction);
		}
	}

	private void SetDirection(Vector2 direction)
	{
		Direction = direction;

		collisionDetector.AdjustRotation(Direction);
	}
	
	public void EnableCollisionDetection()
	{
		detectedCollision = false;
		movementSpeed = lastMovementSpeed;
	}

	private void DisableCollisionDetection()
	{
		timer.ResetTimer();

		detectedCollision = true;
		lastMovementSpeed = movementSpeed;
		movementSpeed = 0f;
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

	private void Start() => SetDirection(Vector2.down);
	private bool LastDirectionIsNotZero() => lastDirection != Vector2.zero;

	private Vector2 RandomDirection()
	{
		List<Vector2> directions = new List<Vector2>{Vector2.up, Vector2.down, Vector2.left, Vector2.right};
		Vector2 start = transform.position;

		directions.RemoveAll(e => Physics2D.Linecast(start, start + e*linecastDetectionDistance, linecastDetectionLayers));

		int randomIndex = Random.Range(0, directions.Count);
		Vector2 randomDirection = directions[randomIndex];

		if(directions.Count > 1 && randomDirection == Direction)
		{
			return RandomDirection();
		}

		return randomDirection;
	}

	private void DetectObstacles()
	{
		if(detectedCollision || freeze.Frozen)
		{
			return;
		}
		
		Collider2D[] colliders = collisionDetector.OverlapBoxAll();

		if(colliders.Length > 1)
		{
			DisableCollisionDetection();
		}
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