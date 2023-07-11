using UnityEngine;

public class PlayerRobotAnimator : EntityAnimator
{
	protected override void SetValues()
	{
		Vector2 movementDirection = movement.Direction;

		animator.SetFloat("MovementSpeed", MovementSpeed(movementDirection));

		if(movementDirection != Vector2.zero)
		{
			animator.SetInteger("MovementX", (int)movementDirection.x);
			animator.SetInteger("MovementY", (int)movementDirection.y);
		}
	}

	private float MovementSpeed(Vector2 movementDirection) => (movementDirection.x != 0f || movementDirection.y != 0f) ? 1f : 0f;
}