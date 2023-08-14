using UnityEngine;

public class BulletStats : MonoBehaviour
{
	[Min(1)] public int damage;
	[Min(0.01f)] public float speed;
	public bool canDestroyMetal;

	private void Start() => SetMovementSpeed();

	private void SetMovementSpeed()
	{
		if(TryGetComponent(out EntityMovement em))
		{
			em.movementSpeed = speed;
		}
	}
}