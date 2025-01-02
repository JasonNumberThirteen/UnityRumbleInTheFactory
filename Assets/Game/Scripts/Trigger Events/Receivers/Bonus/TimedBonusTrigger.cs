using UnityEngine;

public abstract class TimedBonusTrigger : BonusTrigger
{
	[SerializeField, Min(0.01f)] private float duration = 10f;

	public float GetDuration() => duration;
}