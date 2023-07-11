using UnityEngine;

public class EnemyRobotAnimator : MonoBehaviour
{
	private EnemyRobotMovement movement;
	private Animator animator;

	private void Awake()
	{
		movement = GetComponent<EnemyRobotMovement>();
		animator = GetComponent<Animator>();
	}

	private void Update() => SetValues();

	private void SetValues()
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