using UnityEngine;
using UnityEngine.Events;

public class StageEventsManager : MonoBehaviour
{
	public UnityEvent<StageEventType, GameObject> eventReceivedEvent;

	public void SendEvent(StageEventType stageEventType, GameObject sender)
	{
		eventReceivedEvent?.Invoke(stageEventType, sender);
	}
}