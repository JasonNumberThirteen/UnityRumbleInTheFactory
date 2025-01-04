using UnityEngine;

public class EmptyParentGameObjectDestroyer : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float childCheckDelay = 5f;
	
	private void Start()
	{
		InvokeRepeating(nameof(DestroyIfGOHasNoChildren), childCheckDelay, childCheckDelay);
	}

	private void DestroyIfGOHasNoChildren()
	{
		if(gameObject.transform.childCount == 0)
		{
			Destroy(gameObject);
		}
	}
}