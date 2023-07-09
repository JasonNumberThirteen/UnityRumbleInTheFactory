using UnityEngine;

public class PlayerRobotMovement : MonoBehaviour
{
	[Min(0.01f)] public float movementSpeed = 5f;

	private PlayerRobotInput input;
	private Rigidbody2D rb2D;

	public Vector2 MovementVector()
	{
		int x = Mathf.RoundToInt(input.MovementVector.x);
		int y = Mathf.RoundToInt(input.MovementVector.y);

		return new Vector2(x, y);
	}

	private void Awake()
	{
		input = GetComponent<PlayerRobotInput>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + MovementVector()*speed);
	}
}