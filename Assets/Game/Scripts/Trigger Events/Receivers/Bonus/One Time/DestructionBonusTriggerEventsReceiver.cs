using System.Linq;
using UnityEngine;

public class DestructionBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	protected override void GiveEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			DestroyAllActiveRobotEntities(!robotEntity.IsFriendly());
		}
	}

	private void DestroyAllActiveRobotEntities(bool destroyFriendly)
	{
		var activeRobotEntitiesToDestroy = ObjectMethods.FindComponentsOfType<RobotEntity>(false).Where(robotEntity => robotEntity.IsFriendly() == destroyFriendly).ToArray();
		
		activeRobotEntitiesToDestroy.ForEach(DestroyRobotEntity);
		PlaySoundIfNeeded(activeRobotEntitiesToDestroy.Length > 0);
	}

	private void DestroyRobotEntity(RobotEntity robotEntity)
	{
		if(robotEntity != null && robotEntity.TryGetComponent(out EntityExploder entityExploder))
		{
			entityExploder.TriggerExplosion();
		}
	}

	private void PlaySoundIfNeeded(bool playSound)
	{
		if(stageSoundManager != null && playSound)
		{
			stageSoundManager.PlaySound(SoundEffectType.EnemyRobotExplosion);
		}
	}
}