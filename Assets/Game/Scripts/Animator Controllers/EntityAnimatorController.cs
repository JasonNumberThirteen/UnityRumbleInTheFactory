using UnityEngine;

[RequireComponent(typeof(EntityMovementController), typeof(Animator))]
public abstract class EntityAnimatorController : MonoBehaviour
{
	protected readonly string HORIZONTAL_MOVEMENT_PARAMETER_NAME = "MovementX";
	protected readonly string VERTICAL_MOVEMENT_PARAMETER_NAME = "MovementY";
	
	protected EntityMovementController entityMovementController;
	protected Animator animator;

	public Vector2 GetMovementDirection()
	{
		var x = animator.GetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME);
		var y = animator.GetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME);

		return new Vector2(x, y);
	}

	protected virtual void Awake()
	{
		entityMovementController = GetComponent<EntityMovementController>();
		animator = GetComponent<Animator>();
	}

	protected void UpdateMovementParametersValues()
	{
		var movementDirection = entityMovementController.CurrentMovementDirection;
		var adjustedMovementDirection = Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y) ? Vector2.right*movementDirection.x : Vector2.up*movementDirection.y;

		animator.SetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME, Mathf.RoundToInt(adjustedMovementDirection.x));
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, Mathf.RoundToInt(adjustedMovementDirection.y));
	}
}