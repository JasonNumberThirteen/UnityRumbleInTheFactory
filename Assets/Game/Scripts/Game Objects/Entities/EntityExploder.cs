using UnityEngine;

public class EntityExploder : MonoBehaviour
{
	[SerializeField] private GameObject explosionEffectPrefab;
	[SerializeField] private bool destroyGOAfterExplosion = true;

	public void TriggerExplosion()
	{
		SpawnExplosionEffect();
		DestroyGOIfNeeded();
	}

	private void SpawnExplosionEffect()
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