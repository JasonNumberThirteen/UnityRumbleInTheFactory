public class RobotAnimator : EntityAnimator
{
	protected override void SetValues()
	{
		animator.SetFloat("MovementSpeed", MovementSpeed());

		if(!movement.DirectionIsZero())
		{
			animator.SetInteger("MovementX", (int)movement.Direction.x);
			animator.SetInteger("MovementY", (int)movement.Direction.y);
		}
	}

	private float MovementSpeed() => movement.DirectionIsZero() ? 0f : 1f;
}