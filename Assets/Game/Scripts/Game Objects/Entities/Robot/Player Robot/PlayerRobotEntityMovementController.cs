using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInput), typeof(PlayerRobotEntityRankController))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public bool IsSliding {get; set;}

	private PlayerRobotEntityInput playerRobotEntityInput;
	private PlayerRobotEntityRankController playerRobotEntityRankController;

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityInput = GetComponent<PlayerRobotEntityInput>();
		playerRobotEntityRankController = GetComponent<PlayerRobotEntityRankController>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			playerRobotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			playerRobotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnRankChanged(PlayerRobotRank playerRobotRank)
	{
		SetMovementSpeed(playerRobotRank.GetMovementSpeed());
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