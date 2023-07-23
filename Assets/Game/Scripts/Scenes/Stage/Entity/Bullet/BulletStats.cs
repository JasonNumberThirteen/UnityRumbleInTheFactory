using UnityEngine;

public class BulletStats : MonoBehaviour
{
	[Min(1)] public int damage;
	[Min(0.01f)] public float speed;
	public bool canDestroyMetal;

	private void Start()
	{
		EntityMovement em = GetComponent<EntityMovement>();

		if(em != null)
		{
			em.movementSpeed = speed;
		}
	}
}