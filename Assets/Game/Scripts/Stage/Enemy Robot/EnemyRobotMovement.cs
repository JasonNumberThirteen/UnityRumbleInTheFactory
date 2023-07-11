using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotMovement : MonoBehaviour
{
	[Min(0f)] public float movementSpeed;
	
	private Rigidbody2D rb2D;
	private Vector2 direction = Vector2.up;

	public void RandomiseDirection()
	{
		float randomValue = Random.value;

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
	}

	private void Awake() => rb2D = GetComponent<Rigidbody2D>();

	private void FixedUpdate()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + direction*speed);
	}
}