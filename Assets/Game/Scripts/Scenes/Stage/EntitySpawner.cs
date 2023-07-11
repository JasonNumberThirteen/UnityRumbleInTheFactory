using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
	public GameObject entity;
	public string parentTag;

	public void Spawn()
	{
		GameObject instance = Instantiate(entity, gameObject.transform.position, Quaternion.identity);

		instance.transform.SetParent(GameObject.FindGameObjectWithTag(parentTag).transform);
	}
}