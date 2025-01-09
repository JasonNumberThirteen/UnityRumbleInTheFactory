using UnityEngine;

public class MetalTileTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private StageSoundManager stageSoundManager;
	
	public void TriggerOnEnter(GameObject sender)
	{
		if(CanBeDestroyedByBullet(sender))
		{
			Destroy(gameObject);
		}
		else if(stageSoundManager != null && sender != null && sender.TryGetComponent(out PlayerRobotEntityBulletEntity _))
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotBulletHit);
		}
	}

	private void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out BulletEntity bulletEntity) && bulletEntity.CanDestroyMetal();
}