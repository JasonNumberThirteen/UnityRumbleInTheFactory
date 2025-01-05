using UnityEngine;

public class WallTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private StageSoundManager stageSoundManager;
	
	public void TriggerOnEnter(GameObject sender)
	{
		if(stageSoundManager != null && sender != null && sender.TryGetComponent(out PlayerRobotEntityBulletEntity _))
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotBulletHit);
		}
	}

	private void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}
}