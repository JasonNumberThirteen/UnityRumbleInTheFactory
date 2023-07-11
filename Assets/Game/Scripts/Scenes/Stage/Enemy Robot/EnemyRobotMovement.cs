using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotMovement : MonoBehaviour
{
	[Min(0f)] public float movementSpeed;
	[Min(0.01f)] public float rayDistance = 0.5f;
	[Range(1, 90)] public int raycastAngle = 30;
	public LayerMask raycastExcludedLayers;
	
	private Rigidbody2D rb2D;
	private Vector2 direction;

	public Vector2 MovementVector() => direction;

	public void RandomiseDirection()
	{
		float randomValue = Random.value;
		Vector2 currentDirection = direction;

		if(randomValue <= 0.25f)
		{
			direction = Vector2.up;
		}
		else if(randomValue <= 0.5f)
		{
			direction = Vector2.down;
		}
		else if(randomValue <= 0.75f)
		{
			direction = Vector2.left;
		}
		else if(randomValue <= 1f)
		{
			direction = Vector2.right;
		}

		if(currentDirection == direction)
		{
			RandomiseDirection();
		}
	}

	private void Awake() => rb2D = GetComponent<Rigidbody2D>();
	private void Start() => RandomiseDirection();

	private void FixedUpdate()
	{
		Move();
		DetectObstacles();
	}

	private void Move()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + direction*speed);
	}

	private void DetectObstacles()
	{
		for (int angle = -raycastAngle; angle <= raycastAngle; angle += raycastAngle)
		{
			Vector2 rayDirection = Quaternion.Euler(0, 0, angle)*direction;
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