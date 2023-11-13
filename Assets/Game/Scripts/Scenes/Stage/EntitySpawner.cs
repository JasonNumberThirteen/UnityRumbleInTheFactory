using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
	public GameObject entity;
	public string parentTag;

	public virtual void Spawn()
	{
		GameObject parent = GameObject.FindGameObjectWithTag(parentTag);

		if(parent != null)
		{
			EntityInstance().transform.SetParent(parent.transform);
		}
	}

	protected virtual GameObject EntityInstance() => Instantiate(entity, gameObject.transform.position, Quaternion.identity);
}