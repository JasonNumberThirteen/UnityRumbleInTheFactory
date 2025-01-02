using UnityEngine;

public class EntityExploder : MonoBehaviour
{
	[SerializeField] private GameObject explosionEffectPrefab;
	[SerializeField] private bool destroyGOAfterExplosion = true;

	public void TriggerExplosion()
	{
		InstantiateEffectPrefab();
		DestroyGOIfNeeded();
	}

	private void InstantiateEffectPrefab()
	{
		if(explosionEffectPrefab != null)
		{
			Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
		}
	}
	
	private void DestroyGOIfNeeded()
	{
		if(destroyGOAfterExplosion)
		{
			Destroy(gameObject);
		}
	}
}