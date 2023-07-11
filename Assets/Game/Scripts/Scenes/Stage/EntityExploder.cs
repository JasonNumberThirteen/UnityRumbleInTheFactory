using UnityEngine;

public class EntityExploder : MonoBehaviour
{
	public GameObject explosionEffect;
	public bool removesComponent, destroysEntity;

	public void Explode()
	{
		Instantiate(explosionEffect, transform.position, Quaternion.identity);
		RemoveComponent();
		DestroyEntity();
	}

	private void RemoveComponent()
	{
		if(removesComponent)
		{
			Destroy(this);
		}
	}
	
	private void DestroyEntity()
	{
		if(destroysEntity)
		{
			Destroy(gameObject);
		}
	}
}