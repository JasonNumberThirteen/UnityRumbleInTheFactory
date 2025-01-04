using UnityEngine;

public class PlayerRobotEntityMovement : RobotEntityMovement
{
	public bool IsSliding {get; set;}

	private PlayerRobotInput input;

	public Vector2 MovementDirection()
	{
		if(IsSliding)
		{
			return input.LastMovementVector;
		}
		
		Vector2 movementVector = MovementVector();

		return PressedHorizontalMovementKey(movementVector) ? Vector2.right*movementVector.x : Vector2.up*movementVector.y;
	}

	protected override void Awake()
	{
		base.Awake();

		input = GetComponent<PlayerRobotInput>();
	}

	protected virtual void Update()
	{
		UpdateLastDirection();
		UpdateDirection();
		UpdateCollisionDetector();
		LockMovementWhenHitObject();
	}

	private void UpdateDirection() => CurrentMovementDirection = MovementDirection();
	private int MovementAxis(float a) => Mathf.RoundToInt(a);
	private bool PressedHorizontalMovementKey(Vector2 movement) => Mathf.Abs(movement.x) > Mathf.Abs(movement.y);
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;

	private void UpdateLastDirection()
	{
		if(IsMovingInDifferentDirection())
		{
			lastDirection = CurrentMovementDirection;
		}
	}

	private Vector2 MovementVector()
	{
		int x = MovementAxis(input.MovementVector.x);
		int y = MovementAxis(input.MovementVector.y);

		return new Vector2(x, y);
	}

	private void UpdateCollisionDetector()
	{
		if(IsMovingInDifferentDirection())
		{
			robotRotation.RotateByDirection(CurrentMovementDirection);
		}
	}
	
	private void LockMovementWhenHitObject()
	{
		if(robotCollisionDetector.OverlapBox() != null)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else if(rb2D.constraints != RigidbodyConstraints2D.FreezeRotation)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}