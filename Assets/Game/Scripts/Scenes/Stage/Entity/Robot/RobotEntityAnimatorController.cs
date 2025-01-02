public class RobotEntityAnimatorController : EntityAnimatorController
{
	private readonly string MOVEMENT_SPEED_PARAMETER_NAME = "MovementSpeed";
	private readonly string HORIZONTAL_MOVEMENT_PARAMETER_NAME = "MovementX";
	private readonly string VERTICAL_MOVEMENT_PARAMETER_NAME = "MovementY";
	
	private void Update()
	{
		animator.SetFloat(MOVEMENT_SPEED_PARAMETER_NAME, GetCurrentMovementSpeed());

		if(entityMovement.CurrentMovementDirectionIsNone())
		{
			return;
		}

		animator.SetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME, (int)entityMovement.CurrentMovementDirection.x);
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, (int)entityMovement.CurrentMovementDirection.y);
	}

	private float GetCurrentMovementSpeed() => entityMovement.CurrentMovementDirectionIsNone() ? 0f : 1f;
}