using UnityEngine;

public class IntersectingGameObjectTriggerEventsReceiver : MonoBehaviour, ITriggerableOnExit
{
	private int initialLayer;

	public void TriggerOnExit(GameObject sender)
	{
		if(sender == null || !sender.TryGetComponent(out IntersectingGameObjectTriggerEventsReceiver intersectingGameObjectTriggerEventsReceiver))
		{
			return;
		}

		RestoreInitialLayerIfNeeded();
		intersectingGameObjectTriggerEventsReceiver.RestoreInitialLayerIfNeeded();
	}

	private void Awake()
	{
		initialLayer = gameObject.layer;
	}

	private void RestoreInitialLayerIfNeeded()
	{
		if(gameObject.layer != initialLayer)
		{
			gameObject.layer = initialLayer;
		}
	}
}