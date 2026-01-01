using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovementController : MonoBehaviour
{
	public UnityEvent<Vector2> movementDirectionWasChangedEvent;
	
	public Vector2 CurrentMovementDirection
	{
		get
		{
			return currentMovementDirection;
		}
		set
		{
			var currentMovementDirection = this.currentMovementDirection;

			if(currentMovementDirection == value)
			{
				return;
			}

			this.currentMovementDirection = value;

			movementDirectionWasChangedEvent?.Invoke(this.currentMovementDirection);
		}
	}

	protected Rigidbody2D rb2D;
	protected float movementSpeed;

	private Vector2 currentMovementDirection;
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

	protected virtual void OnEnable()
	{
		CurrentMovementDirection = lastMovementDirection;
	}

	protected virtual void OnDisable()
	{
		lastMovementDirection = CurrentMovementDirection;
		CurrentMovementDirection = Vector2.zero;
	}

	private void FixedUpdate()
	{
		var movementSpeedPerFrame = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + CurrentMovementDirection*movementSpeedPerFrame);
	}
}