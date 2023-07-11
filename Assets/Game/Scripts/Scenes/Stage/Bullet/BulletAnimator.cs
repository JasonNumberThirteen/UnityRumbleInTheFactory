using UnityEngine;

public class BulletAnimator : EntityAnimator
{
	protected override void SetValues()
	{
		Vector2 direction = movement.Direction;

		animator.SetInteger("MovementX", (int)direction.x);
		animator.SetInteger("MovementY", (int)direction.y);
	}
}