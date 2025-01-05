using UnityEngine;

public class RobotEntityAnimatorController : EntityAnimatorController
{
	[SerializeField] private VerticalDirection initialVerticalDirection = VerticalDirection.Up;
	
	private readonly string MOVEMENT_SPEED_PARAMETER_NAME = "MovementSpeed";

	private void Start()
	{
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, GetInitialVerticalDirection());
	}

	private int GetInitialVerticalDirection()
	{
		return initialVerticalDirection switch
		{
			VerticalDirection.Up => 1,
			VerticalDirection.Down => -1,
			_ => 0
		};
	}
	
	private void Update()
	{
		animator.SetFloat(MOVEMENT_SPEED_PARAMETER_NAME, GetCurrentMovementSpeed());

		if(!entityMovement.CurrentMovementDirectionIsNone())
		{
			UpdateMovementParametersValues();
		}
	}

	private float GetCurrentMovementSpeed() => entityMovement.CurrentMovementDirectionIsNone() ? 0f : 1f;
}