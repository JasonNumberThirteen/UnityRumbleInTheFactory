using UnityEngine;

public class LifeBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	[SerializeField, Min(1)] private int lives = 1;

	protected override bool ShouldPlaySound(GameObject sender) => !sender.TryGetComponent(out PlayerRobotEntity _);

	protected override void GiveEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntity.OnLifeBonusCollected(lives);
		}
	}
}