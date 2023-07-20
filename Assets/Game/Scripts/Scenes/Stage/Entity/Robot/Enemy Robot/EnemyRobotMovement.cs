using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotMovement : EntityMovement
{
	[Min(0.01f)] public float rayDistance = 0.5f;
	[Range(1, 90)] public int raycastAngle = 30;
	public LayerMask raycastExcludedLayers;
	public Timer timer;

	private bool detectedCollision;
	private EnemyRobotFreeze freeze;

	public void RandomiseDirection() => Direction = RandomDirection();
	public void EnableCollisionDetection() => detectedCollision = false;

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

	private void Start() => Direction = Vector2.down;

	private Vector2 RandomDirection()
	{
		Vector2[] directions = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};
		int randomIndex = Random.Range(0, directions.Length - 1);
		Vector2 randomDirection = directions[randomIndex];

		if(randomDirection == Direction)
		{
			return RandomDirection();
		}

		return randomDirection;
	}
	
	private void DetectObstacles()
	{
		for (int angle = -raycastAngle; angle <= raycastAngle; angle += raycastAngle)
		{
			Quaternion rayRotation = Quaternion.Euler(0, 0, angle);
			Vector2 rayDirection = rayRotation*Direction;
			Vector2 rayPosition = rayDirection*rayDistance;
			RaycastHit2D hit = Physics2D.Raycast(rb2D.position, rayDirection, rayDistance, ~raycastExcludedLayers);

			Debug.DrawLine(rb2D.position, rb2D.position + rayPosition, Color.red);

			if(hit.collider != null && !detectedCollision && !freeze.Frozen)
			{
				timer.ResetTimer();

				detectedCollision = true;
			}
		}
	}
}