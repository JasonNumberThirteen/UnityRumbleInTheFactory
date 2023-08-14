using UnityEngine;

public class BricksParentDestroyer : MonoBehaviour
{
	[Min(0.01f)] public float childCheckDelay = 5f;
	
	private void Start() => InvokeRepeating(nameof(CheckChildCount), childCheckDelay, childCheckDelay);

	private void CheckChildCount()
	{
		if(gameObject.transform.childCount == 0)
		{
			Destroy(gameObject);
		}
	}
}