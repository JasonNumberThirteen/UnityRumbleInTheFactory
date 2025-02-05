using UnityEngine;

public class IntersectingGameObjectTriggerEventsReceiver : MonoBehaviour, ITriggerableOnExit
{
	private int initialLayer;

	public int GetInitialLayer() => initialLayer;
	
	public void TriggerOnExit(GameObject sender)
	{
		if(sender != null && sender.TryGetComponent(out IntersectingGameObjectTriggerEventsReceiver intersectingGameObjectTriggerEventsReceiver))
		{
			RestoreInitialLayerIfNeeded();
			intersectingGameObjectTriggerEventsReceiver.RestoreInitialLayerIfNeeded();
		}
	}

	public void RestoreInitialLayerIfNeeded()
	{
		if(gameObject.layer != initialLayer)
		{
			gameObject.layer = initialLayer;
		}
	}

	private void Awake()
	{
		initialLayer = gameObject.layer;
	}
}