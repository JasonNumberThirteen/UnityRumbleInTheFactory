using UnityEngine;

public class MetalDestructibleTileTriggerEventsReceiver : DestructibleTileTriggerEventsReceiver
{
	private StageSoundManager stageSoundManager;
	
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

	protected override void Awake()
	{
		base.Awake();
		
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out BulletEntity bulletEntity) && bulletEntity.CanDestroyMetal();
}