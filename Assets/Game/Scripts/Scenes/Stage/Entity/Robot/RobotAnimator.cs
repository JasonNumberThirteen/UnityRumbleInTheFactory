using UnityEngine;

public class RobotAnimator : EntityAnimator
{
	protected override void SetValues()
	{
		Vector2 direction = movement.Direction;

		animator.SetFloat("MovementSpeed", MovementSpeed(direction));

		if(direction != Vector2.zero)
		{
			animator.SetInteger("MovementX", (int)direction.x);
			animator.SetInteger("MovementY", (int)direction.y);
		}
	}

	private float MovementSpeed(Vector2 direction) => (direction == Vector2.zero) ? 0f : 1f;
}