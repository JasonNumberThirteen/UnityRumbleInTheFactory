public class RobotEntityAnimator : EntityAnimator
{
	private void Update()
	{
		animator.SetFloat("MovementSpeed", MovementSpeed());

		if(!movement.CurrentMovementDirectionIsNone())
		{
			animator.SetInteger("MovementX", (int)movement.CurrentMovementDirection.x);
			animator.SetInteger("MovementY", (int)movement.CurrentMovementDirection.y);
		}
	}

	private float MovementSpeed() => movement.CurrentMovementDirectionIsNone() ? 0f : 1f;
}