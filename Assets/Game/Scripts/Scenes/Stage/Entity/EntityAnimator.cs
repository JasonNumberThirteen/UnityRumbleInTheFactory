using UnityEngine;

[RequireComponent(typeof(EntityMovement), typeof(Animator))]
public abstract class EntityAnimator : MonoBehaviour
{
	protected EntityMovement entityMovement;
	protected Animator animator;

	protected virtual void Awake()
	{
		entityMovement = GetComponent<EntityMovement>();
		animator = GetComponent<Animator>();
	}
}