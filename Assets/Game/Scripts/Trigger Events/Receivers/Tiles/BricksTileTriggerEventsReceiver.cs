using UnityEngine;

public class BricksTileTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		Destroy(gameObject);
	}
}