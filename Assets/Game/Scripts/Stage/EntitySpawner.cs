using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
	public GameObject entity;
	public Transform parent;

	public void Spawn()
	{
		GameObject instance = Instantiate(entity, gameObject.transform.position, Quaternion.identity);

		instance.transform.SetParent(parent);
	}
}