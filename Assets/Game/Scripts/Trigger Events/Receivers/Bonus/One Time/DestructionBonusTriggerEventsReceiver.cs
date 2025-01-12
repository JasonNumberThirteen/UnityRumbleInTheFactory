using System.Linq;
using UnityEngine;

public class DestructionBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	protected override void GiveEffect(GameObject sender)
	{
		DestroyAllRobotEntities(sender.TryGetComponent(out RobotEntity robotEntity) && !robotEntity.IsFriendly());
	}

	private void DestroyAllRobotEntities(bool destroyFriendly)
	{
		var robotEntities = FindObjectsByType<RobotEntity>(FindObjectsSortMode.None).Where(robotEntity => robotEntity.IsFriendly() == destroyFriendly).ToArray();
		
		foreach (var robotEntity in robotEntities)
		{
			DestroyRobotEntity(robotEntity);
		}

		PlayExplosionSoundIfNeeded(robotEntities.Length > 0);
	}

	private void DestroyRobotEntity(RobotEntity robotEntity)
	{
		if(robotEntity != null && robotEntity.TryGetComponent(out EntityExploder entityExploder))
		{
			entityExploder.TriggerExplosion();
		}
	}

	private void PlayExplosionSoundIfNeeded(bool playSound)
	{
		if(stageSoundManager != null && playSound)
		{
			stageSoundManager.PlaySound(SoundEffectType.EnemyRobotExplosion);
		}
	}
}