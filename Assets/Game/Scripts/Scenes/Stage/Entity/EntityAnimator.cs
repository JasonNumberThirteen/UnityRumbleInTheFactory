using UnityEngine;

[RequireComponent(typeof(EntityMovement), typeof(Animator))]
public abstract class EntityAnimator : MonoBehaviour
{
	protected EntityMovement movement;
	protected Animator animator;

	protected virtual void Awake()
	{
		movement = GetComponent<EntityMovement>();
		animator = GetComponent<Animator>();
	}
}