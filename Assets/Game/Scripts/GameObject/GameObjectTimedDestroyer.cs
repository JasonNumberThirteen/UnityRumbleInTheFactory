using UnityEngine;

public class GameObjectTimedDestroyer : MonoBehaviour
{
	public void Destroy(float delay)
	{
		Destroy(gameObject, delay);
	}
}