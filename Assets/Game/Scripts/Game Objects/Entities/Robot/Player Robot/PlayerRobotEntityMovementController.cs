using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInput))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public bool IsSliding {get; set;}

	private PlayerRobotEntityInput playerRobotEntityInput;

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityInput = GetComponent<PlayerRobotEntityInput>();
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
		CurrentMovementDirection = IsSliding ? playerRobotEntityInput.LastMovementVector : GetMovementDirection();
	}

	private Vector2 GetMovementDirection()
	{
		var movementVector = GetMovementVector();

		return PressedHorizontalMovementKey(movementVector) ? Vector2.right*movementVector.x : Vector2.up*movementVector.y;
	}

	private Vector2 GetMovementVector()
	{
		var x = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.x);
		var y = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.y);

		return new Vector2(x, y);
	}

	private void RotateByDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			robotEntityCollisionDetector.AdjustRotationIfPossible(CurrentMovementDirection);
		}
	}
	
	private void LockMovementWhenHitObject()
	{
		if(robotEntityCollisionDetector != null && robotEntityCollisionDetector.OverlapBox() != null)
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