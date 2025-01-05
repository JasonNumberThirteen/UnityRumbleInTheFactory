using UnityEngine;

[RequireComponent(typeof(EntityMovementController), typeof(Animator))]
public abstract class EntityAnimatorController : MonoBehaviour
{
	protected readonly string HORIZONTAL_MOVEMENT_PARAMETER_NAME = "MovementX";
	protected readonly string VERTICAL_MOVEMENT_PARAMETER_NAME = "MovementY";
	
	protected EntityMovementController entityMovementController;
	protected Animator animator;

	protected virtual void Awake()
	{
		entityMovementController = GetComponent<EntityMovementController>();
		animator = GetComponent<Animator>();
	}

	protected void UpdateMovementParametersValues()
	{
		var movementDirection = entityMovementController.CurrentMovementDirection;
		
		animator.SetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME, (int)movementDirection.x);
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, (int)movementDirection.y);
	}
}