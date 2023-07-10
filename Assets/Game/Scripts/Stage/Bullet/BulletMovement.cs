using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	[Min(0.01f)] public float movementSpeed = 5f;

	public Vector2 Direction {get; private set;} = Vector2.up;

	private Rigidbody2D rb2D;

	public void SetDirection(Vector2 direction)
	{
		Direction = direction;
	}

	private void Awake() => rb2D = GetComponent<Rigidbody2D>();
	
	private void FixedUpdate()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + speed*Direction);
	}
}