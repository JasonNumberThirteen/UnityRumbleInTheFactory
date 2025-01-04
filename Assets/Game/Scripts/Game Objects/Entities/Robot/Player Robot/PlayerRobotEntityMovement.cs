using UnityEngine;

[RequireComponent(typeof(PlayerRobotInput))]
public class PlayerRobotEntityMovement : RobotEntityMovement
{
	public bool IsSliding {get; set;}

	private PlayerRobotInput playerRobotInput;

	protected override void Awake()
	{
		base.Awake();

		playerRobotInput = GetComponent<PlayerRobotInput>();
	}

	private void Update()
	{
		UpdateLastDirectionIfNeeded();
		UpdateCurrentMovementDirection();
		RotateByDirectionIfNeeded();
		LockMovementWhenHitObject();
	}

	private void UpdateLastDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			lastDirection = CurrentMovementDirection;
		}
	}

	private void UpdateCurrentMovementDirection()
	{
		CurrentMovementDirection = IsSliding ? playerRobotInput.LastMovementVector : GetMovementDirection();
	}

	private Vector2 GetMovementDirection()
	{
		var movementVector = GetMovementVector();

		return PressedHorizontalMovementKey(movementVector) ? Vector2.right*movementVector.x : Vector2.up*movementVector.y;
	}

	private Vector2 GetMovementVector()
	{
		var x = Mathf.RoundToInt(playerRobotInput.MovementVector.x);
		var y = Mathf.RoundToInt(playerRobotInput.MovementVector.y);

		return new Vector2(x, y);
	}

	private void RotateByDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			robotRotation.RotateByDirection(CurrentMovementDirection);
		}
	}
	
	private void LockMovementWhenHitObject()
	{
		if(robotCollisionDetector != null && robotCollisionDetector.OverlapBox() != null)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else if(rb2D.constraints != RigidbodyConstraints2D.FreezeRotation)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	private bool PressedHorizontalMovementKey(Vector2 movement) => Mathf.Abs(movement.x) > Mathf.Abs(movement.y);
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;
}