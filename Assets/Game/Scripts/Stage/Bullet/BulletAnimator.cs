using UnityEngine;

public class BulletAnimator : MonoBehaviour
{
	private BulletMovement movement;
	private Animator animator;

	private void Update() => SetValues();

	private void Awake()
	{
		movement = GetComponent<BulletMovement>();
		animator = GetComponent<Animator>();
	}

	private void SetValues()
	{
		Vector2 direction = movement.Direction;

		animator.SetInteger("MovementX", (int)direction.x);
		animator.SetInteger("MovementY", (int)direction.y);
	}
}