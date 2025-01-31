using UnityEngine;
using UnityEngine.Events;

public class EntityExploder : MonoBehaviour
{
	public UnityEvent entityDestroyedEvent;
	
	[SerializeField] private GameObject explosionEffectPrefab;
	[SerializeField] private bool destroyGOAfterExplosion = true;

	private bool wasDestroyed;

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
		if(!destroyGOAfterExplosion || wasDestroyed)
		{
			return;
		}

		wasDestroyed = true;

		entityDestroyedEvent?.Invoke();
		Destroy(gameObject);
	}
}