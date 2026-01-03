using UnityEngine;

public abstract class TimedBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	[SerializeField, Min(0f)] private float duration = 10f;

	public float GetDuration() => duration;
}