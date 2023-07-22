using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotMovement : EntityMovement
{
	public LayerMask collisionDetectionLayers;
	public Timer timer;
	public Collider2D collisionDetector;

	private bool detectedCollision;
	private EnemyRobotFreeze freeze;
	private float lastMovementSpeed;

	public void RandomiseDirection() => SetDirection(RandomDirection());

	private void SetDirection(Vector2 direction)
	{
		Direction = direction;

		AdjustCollisionDetectorRotation();
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

	private Vector2 RandomDirection()
	{
		Vector2[] directions = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};
		int randomIndex = Random.Range(0, directions.Length);
		Vector2 randomDirection = directions[randomIndex];

		if(randomDirection == Direction)
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
		
		Collider2D[] colliders = Physics2D.OverlapBoxAll(collisionDetector.bounds.center, collisionDetector.bounds.size, 0f, collisionDetectionLayers);

		if(colliders.Length > 1)
		{
			DisableCollisionDetection();
		}
	}

	private void AdjustCollisionDetectorRotation()
	{
		Transform collisionDetectorTransform = collisionDetector.gameObject.transform;
		
		if(Direction == Vector2.up)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else if(Direction == Vector2.down)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 180);
		}
		else if(Direction == Vector2.left)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 90);
		}
		else if(Direction == Vector2.right)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 270);
		}
	}

	private void OnDrawGizmos()
	{
		if(collisionDetector == null)
		{
			return;
		}
		
		Collider2D[] colliders = Physics2D.OverlapBoxAll(collisionDetector.bounds.center, collisionDetector.bounds.size, 0f, collisionDetectionLayers);

		foreach (Collider2D collider in colliders)
		{
			Gizmos.color = Color.red;
			
			Gizmos.DrawWireCube(collider.transform.position, collider.bounds.size);
		}
	}
}