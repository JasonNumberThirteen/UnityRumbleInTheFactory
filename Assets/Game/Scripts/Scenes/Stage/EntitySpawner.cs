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
			GameObject instance = Instantiate(entity, gameObject.transform.position, Quaternion.identity);

			instance.transform.SetParent(parent.transform);
		}
	}
}