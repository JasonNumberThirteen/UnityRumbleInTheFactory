using UnityEngine;

public class BricksTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		Destroy(gameObject);
	}
}