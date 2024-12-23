using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
	public void Destroy(float delay)
	{
		Destroy(gameObject, delay);
	}
}