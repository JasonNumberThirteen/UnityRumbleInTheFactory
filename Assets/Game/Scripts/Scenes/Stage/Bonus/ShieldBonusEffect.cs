using UnityEngine;

public class ShieldBonusEffect : BonusEffect
{
	[Min(0.01f)] public float duration = 10f;
	
	public override void PerformEffect()
	{
		GameObject shield = GameObject.FindGameObjectWithTag("Shield");
		Timer timer = shield.GetComponent<Timer>();

		timer.duration = duration;

		timer.ResetTimer();
	}
}