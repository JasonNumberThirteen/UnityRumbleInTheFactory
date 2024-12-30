using UnityEngine;

public class EntityExploder : MonoBehaviour
{
	public GameObject explosionEffect;
	public bool destroysEntity;

	public void Explode()
	{
		Instantiate(explosionEffect, transform.position, Quaternion.identity);
		DestroyEntity();
	}
	
	private void DestroyEntity()
	{
		if(destroysEntity)
		{
			Destroy(gameObject);
		}
	}
}