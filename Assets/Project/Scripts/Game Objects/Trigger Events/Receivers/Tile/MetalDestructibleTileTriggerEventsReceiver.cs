using UnityEngine;

public class MetalDestructibleTileTriggerEventsReceiver : DestructibleTileTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(CanBeDestroyedByBullet(sender))
		{
			base.TriggerOnEnter(sender);
		}
		else if(stageSoundManager != null && sender != null && sender.TryGetComponent(out PlayerRobotEntityBulletEntity _))
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotBulletHit);
		}
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out BulletEntity bulletEntity) && bulletEntity.CanDestroyMetal();
}