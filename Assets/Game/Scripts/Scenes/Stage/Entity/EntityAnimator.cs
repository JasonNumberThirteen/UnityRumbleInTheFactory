using UnityEngine;

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