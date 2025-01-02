public class BulletAnimator : EntityAnimatorController
{
	private readonly string HORIZONTAL_MOVEMENT_PARAMETER_NAME = "MovementX";
	private readonly string VERTICAL_MOVEMENT_PARAMETER_NAME = "MovementY";
	
	private void Start()
	{
		var movementDirection = entityMovement.CurrentMovementDirection;

		animator.SetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME, (int)movementDirection.x);
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, (int)movementDirection.y);
	}
}