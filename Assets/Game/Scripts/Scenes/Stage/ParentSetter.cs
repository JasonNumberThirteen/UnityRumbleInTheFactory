using UnityEngine;

public class ParentSetter : MonoBehaviour
{
	public string parentTag;

	private void Start()
	{
		GameObject parent = FoundParent();

		if(parent != null)
		{
			gameObject.transform.SetParent(parent.transform);
		}
	}

	private GameObject FoundParent() => GameObject.FindGameObjectWithTag(parentTag);
}