using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovementController : MonoBehaviour
{
	public Vector2 CurrentMovementDirection {get; set;}

	protected Rigidbody2D rb2D;
	protected float movementSpeed;

	private Vector2 lastMovementDirection;

	public bool CurrentMovementDirectionIsNone() => CurrentMovementDirection.IsZero();

	public void SetMovementSpeed(float movementSpeed)
	{
		this.movementSpeed = movementSpeed;
	}

	protected virtual void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		var movementSpeedPerFrame = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + CurrentMovementDirection*movementSpeedPerFrame);
	}

	private void OnDisable()
	{
		lastMovementDirection = CurrentMovementDirection;
		CurrentMovementDirection = Vector2.zero;
	}

	private void OnEnable()
	{
		CurrentMovementDirection = lastMovementDirection;
	}
}