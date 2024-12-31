using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
	public UnityEvent entitySpawnedEvent;

	[SerializeField] private GameObject entityPrefab;

	public void SetEntityPrefab(GameObject entityPrefab)
	{
		this.entityPrefab = entityPrefab;
	}

	public virtual void Spawn()
	{
		var entityInstance = GetEntityInstance();

		if(entityInstance != null)
		{
			entitySpawnedEvent?.Invoke();
		}
	}

	protected virtual GameObject GetEntityInstance() => entityPrefab != null ? Instantiate(entityPrefab, gameObject.transform.position, Quaternion.identity) : null;
}