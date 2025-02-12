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
		var robotEntities = ObjectMethods.FindComponentsOfType<RobotEntity>(false).Where(robotEntity => robotEntity.IsFriendly() == destroyFriendly).ToArray();
		
		robotEntities.ForEach(DestroyRobotEntity);
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