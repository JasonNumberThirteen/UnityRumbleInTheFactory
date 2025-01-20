using UnityEngine;

public class LifeBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	[SerializeField, Min(1)] private int lives = 1;

	protected override SoundEffectType GetSoundEffectType(GameObject sender) => sender.TryGetComponent(out PlayerRobotEntity _) ? SoundEffectType.PlayerRobotLifeGain : base.GetSoundEffectType(sender);

	protected override void GiveEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntity.OnLifeBonusCollected(lives);
		}
	}
}