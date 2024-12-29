using UnityEngine;

public class BricksTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerEffect(GameObject sender)
	{
		Destroy(gameObject);
	}
}