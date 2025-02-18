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
		var oneDimensionalMovementDirection = entityMovementController.CurrentMovementDirection.GetOneDimensionalVector();

		animator.SetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME, Mathf.RoundToInt(oneDimensionalMovementDirection.x));
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, Mathf.RoundToInt(oneDimensionalMovementDirection.y));
	}
}