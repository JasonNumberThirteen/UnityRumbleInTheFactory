using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotMovement : MonoBehaviour
{
	[Min(0f)] public float movementSpeed;
	[Min(0.01f)] public float rayDistance = 0.5f;
	public LayerMask raycastExcludedLayers;
	
	private Rigidbody2D rb2D;
	private Vector2 direction;
	
	public Vector2 MovementVector() => direction;

	private void Awake() => rb2D = GetComponent<Rigidbody2D>();
	private void Start() => RandomiseDirection();

	private void FixedUpdate()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;
		Vector2 rayPosition = direction*rayDistance;

		rb2D.MovePosition(rb2D.position + direction*speed);

		RaycastHit2D hit = Physics2D.Raycast(rb2D.position, direction, rayDistance, ~raycastExcludedLayers);

		if(hit.collider != null)
		{
			RandomiseDirection();
		}

		Debug.DrawLine(rb2D.position, rb2D.position + rayPosition, Color.red);
	}

	private void RandomiseDirection()
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
}