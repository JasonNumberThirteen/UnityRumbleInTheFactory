using UnityEngine;

public class GameObjectTaggedParentSetter : MonoBehaviour
{
	[SerializeField] private string parentTag;

	private void Start()
	{
		var parent = GameObject.FindGameObjectWithTag(parentTag);

		if(parent != null)
		{
			gameObject.transform.SetParent(parent.transform);
		}
	}
}