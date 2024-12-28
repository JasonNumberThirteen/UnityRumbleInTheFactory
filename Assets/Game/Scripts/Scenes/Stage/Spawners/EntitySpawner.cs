using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
	public UnityEvent entitySpawnedEvent;
	public GameObject entity;
	public string parentTag;

	public virtual void Spawn()
	{
		GameObject parent = GameObject.FindGameObjectWithTag(parentTag);

		if(parent != null)
		{
			EntityInstance().transform.SetParent(parent.transform);
		}

		entitySpawnedEvent?.Invoke();
	}

	protected virtual GameObject EntityInstance() => Instantiate(entity, gameObject.transform.position, Quaternion.identity);
}