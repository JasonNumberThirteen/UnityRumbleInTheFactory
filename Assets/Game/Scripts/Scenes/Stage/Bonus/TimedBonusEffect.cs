using UnityEngine;

public abstract class TimedBonusEffect : BonusEffect
{
	[Min(0.01f)] public float duration = 10f;
}