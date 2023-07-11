using UnityEngine;

public class BulletAnimator : MonoBehaviour
{
	private EntityMovement movement;
	private Animator animator;

	private void Update() => SetValues();

	private void Awake()
	{
		movement = GetComponent<EntityMovement>();
		animator = GetComponent<Animator>();
	}

	private void SetValues()
	{
		Vector2 direction = movement.Direction;

		animator.SetInteger("MovementX", (int)direction.x);
		animator.SetInteger("MovementY", (int)direction.y);
	}
}