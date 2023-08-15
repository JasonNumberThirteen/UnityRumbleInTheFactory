using UnityEngine;

public class EnemyRobotMovement : EntityMovement
{
	public Timer timer;
	public RobotCollisionDetector collisionDetector;

	private bool detectedCollision;
	private EnemyRobotFreeze freeze;
	private float lastMovementSpeed;
	private Vector2 lastDirection;
	private EnemyRobotMovementDirectionSelector movementDirectionSelector;

	public void SetMovementLock()
	{
		if(LastDirectionIsNotZero())
		{
			SetDirections(Direction, lastDirection);
		}
	}

	public void RandomiseDirection()
	{
		Vector2 direction = movementDirectionSelector.RandomDirection(Direction);

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
		movementDirectionSelector = GetComponent<EnemyRobotMovementDirectionSelector>();
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
	private bool DetectedCollision() => !detectedCollision && !freeze.Frozen && collisionDetector.OverlapBoxAll().Length > 1;
	
	private void DetectObstacles()
	{
		if(DetectedCollision())
		{
			DisableCollisionDetection();
		}
	}

	private void DisableCollisionDetection()
	{
		timer.ResetTimer();

		detectedCollision = true;
		lastMovementSpeed = movementSpeed;
		movementSpeed = 0f;
	}

	private void OnDrawGizmos()
	{
		DrawDetectedColliders();
		DrawAvailableDirections();
	}

	private void DrawDetectedColliders()
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
	}

	private void DrawAvailableDirections()
	{
		if(movementDirectionSelector == null)
		{
			return;
		}

		Vector2[] directions = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

		foreach (Vector2 direction in directions)
		{
			Vector2 start = transform.position;
			Vector2 end = start + direction*movementDirectionSelector.obstacleDetectionDistance;

			Gizmos.color = Color.white;

			if(Physics2D.Linecast(start, end, movementDirectionSelector.obstacleDetectionLayers))
			{
				Gizmos.color = Color.red;
			}
			
			Gizmos.DrawLine(start, end);
		}
	}
}