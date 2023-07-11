using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
	[Min(0f)] public float movementSpeed = 5f;
	
	public Vector2 Direction {get; set;}

	protected Rigidbody2D rb2D;

	protected virtual void Awake() => rb2D = GetComponent<Rigidbody2D>();
	protected virtual void FixedUpdate() => Move();

	protected virtual void Move()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + Direction*speed);
	}
}