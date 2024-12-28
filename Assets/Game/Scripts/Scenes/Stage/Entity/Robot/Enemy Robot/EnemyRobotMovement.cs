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

	public void SetCollisionDetectionState(bool detected)
	{
		if(detected)
		{
			timer.ResetTimer();

			lastMovementSpeed = movementSpeed;
		}

		detectedCollision = detected;
		movementSpeed = detected ? 0f : lastMovementSpeed;
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

	private void Start()
	{
		movementSpeed *= StageManager.instance.gameData.GetDifficultyTierValue(tier => tier.GetEnemyMovementSpeedMultiplier());

		SetDirection(Vector2.down);
	}

	private bool LastDirectionIsNotZero() => lastDirection != Vector2.zero;
	private bool DetectedCollision() => !detectedCollision && !freeze.Frozen && collisionDetector.OverlapBoxAll().Length > 1;
	
	private void DetectObstacles()
	{
		if(DetectedCollision())
		{
			SetCollisionDetectionState(true);
		}
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

		Vector2[] directions = movementDirectionSelector.AllDirections().ToArray();

		foreach (Vector2 direction in directions)
		{
			Vector2 start = transform.position;
			Vector2 end = movementDirectionSelector.LinecastEnd(start, direction);

			Gizmos.color = movementDirectionSelector.Linecast(start, direction) ? Color.red : Color.white;
			
			Gizmos.DrawLine(start, end);
		}
	}
}