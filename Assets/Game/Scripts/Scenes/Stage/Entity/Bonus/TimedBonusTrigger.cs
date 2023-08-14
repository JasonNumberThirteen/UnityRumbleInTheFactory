using UnityEngine;

public abstract class TimedBonusTrigger : BonusTrigger
{
	[Min(0.01f)] public float duration = 10f;
}