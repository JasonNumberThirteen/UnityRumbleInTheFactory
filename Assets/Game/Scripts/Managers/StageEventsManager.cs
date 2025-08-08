using UnityEngine;
using UnityEngine.Events;

public class StageEventsManager : MonoBehaviour
{
	public UnityEvent<StageEvent> eventWasSentEvent;

	public void SendEvent(StageEvent stageEvent)
	{
		eventWasSentEvent?.Invoke(stageEvent);
	}
}