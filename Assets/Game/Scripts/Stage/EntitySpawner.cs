using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
	public GameObject entity;

	public void Spawn()
	{
		GameObject instance = Instantiate(entity, gameObject.transform.position, Quaternion.identity);

		instance.transform.SetParent(GameObject.FindGameObjectWithTag("Entities").transform);
	}
}