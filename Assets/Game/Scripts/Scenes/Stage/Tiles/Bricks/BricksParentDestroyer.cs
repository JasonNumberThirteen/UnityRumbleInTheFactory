using UnityEngine;

public class BricksParentDestroyer : MonoBehaviour
{
	[Min(0.01f)] public float childCheckDelay = 5f;
	
	private void Start() => InvokeRepeating("CheckChildCount", childCheckDelay, childCheckDelay);

	private void CheckChildCount()
	{
		int count = gameObject.transform.childCount;

		if(count == 0)
		{
			Destroy(gameObject);
		}
	}
}