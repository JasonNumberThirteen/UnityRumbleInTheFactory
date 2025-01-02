using UnityEngine;

public class BricksTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		Destroy(gameObject);
	}
}