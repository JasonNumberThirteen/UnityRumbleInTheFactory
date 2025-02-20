using UnityEngine;
using UnityEngine.Events;

public class StageEventsManager : MonoBehaviour
{
	public UnityEvent<StageEvent> eventReceivedEvent;

	public void SendEvent(StageEvent stageEvent)
	{
		eventReceivedEvent?.Invoke(stageEvent);
	}
}