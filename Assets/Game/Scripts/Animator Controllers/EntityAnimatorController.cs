using UnityEngine;

[RequireComponent(typeof(EntityMovement), typeof(Animator))]
public abstract class EntityAnimatorController : MonoBehaviour
{
	protected readonly string HORIZONTAL_MOVEMENT_PARAMETER_NAME = "MovementX";
	protected readonly string VERTICAL_MOVEMENT_PARAMETER_NAME = "MovementY";
	
	protected EntityMovement entityMovement;
	protected Animator animator;

	protected virtual void Awake()
	{
		entityMovement = GetComponent<EntityMovement>();
		animator = GetComponent<Animator>();
	}

	protected void UpdateMovementParametersValues()
	{
		var movementDirection = entityMovement.CurrentMovementDirection;
		
		animator.SetInteger(HORIZONTAL_MOVEMENT_PARAMETER_NAME, (int)movementDirection.x);
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, (int)movementDirection.y);
	}
}