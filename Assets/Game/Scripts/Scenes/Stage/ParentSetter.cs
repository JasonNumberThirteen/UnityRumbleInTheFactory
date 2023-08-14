using UnityEngine;

public class ParentSetter : MonoBehaviour
{
	public string parentTag;

	private void SetParent()
	{
		GameObject parent = FoundParent();

		if(parent != null)
		{
			gameObject.transform.SetParent(parent.transform);
		}
	}

	private void Start() => SetParent();
	private GameObject FoundParent() => GameObject.FindGameObjectWithTag(parentTag);
}