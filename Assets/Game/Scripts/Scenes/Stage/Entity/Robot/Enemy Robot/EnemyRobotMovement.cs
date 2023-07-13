using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotMovement : EntityMovement
{
	[Min(0.01f)] public float rayDistance = 0.5f;
	[Range(1, 90)] public int raycastAngle = 30;
	public LayerMask raycastExcludedLayers;

	public void RandomiseDirection()
	{
		float randomValue = Random.value;
		Vector2 currentDirection = Direction;

		if(randomValue <= 0.25f)
		{
			Direction = Vector2.up;
		}
		else if(randomValue <= 0.5f)
		{
			Direction = Vector2.down;
		}
		else if(randomValue <= 0.75f)
		{
			Direction = Vector2.left;
		}
		else if(randomValue <= 1f)
		{
			Direction = Vector2.right;
		}

		if(currentDirection == Direction)
		{
			RandomiseDirection();
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		DetectObstacles();
	}

	private void Start() => RandomiseDirection();
	
	private void DetectObstacles()
	{
		for (int angle = -raycastAngle; angle <= raycastAngle; angle += raycastAngle)
		{
			Quaternion rayRotation = Quaternion.Euler(0, 0, angle);
			Vector2 rayDirection = rayRotation*Direction;
			Vector2 rayPosition = rayDirection*rayDistance;
			RaycastHit2D hit = Physics2D.Raycast(rb2D.position, rayDirection, rayDistance, ~raycastExcludedLayers);

			Debug.DrawLine(rb2D.position, rb2D.position + rayPosition, Color.red);

			if(hit.collider != null)
			{
				RandomiseDirection();
			}
		}
	}
}