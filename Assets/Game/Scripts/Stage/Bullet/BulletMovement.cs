using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	[Min(0.01f)] public float movementSpeed = 5f;

	private Rigidbody2D rb2D;
	private Vector2 direction = Vector2.up;

	private void Awake() => rb2D = GetComponent<Rigidbody2D>();
	
	private void FixedUpdate()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + speed*direction);
	}
}