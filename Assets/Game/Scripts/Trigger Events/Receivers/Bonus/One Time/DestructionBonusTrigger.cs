using System.Linq;
using UnityEngine;

public class DestructionBonusTrigger : BonusTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		DestroyAllRobots(sender.TryGetComponent(out Robot robot) && !robot.IsFriendly());
		base.TriggerOnEnter(sender);
	}

	private void DestroyAllRobots(bool destroyFriendly)
	{
		var robots = FindObjectsByType<Robot>(FindObjectsSortMode.None).Where(robot => robot.IsFriendly() == destroyFriendly).ToArray();
		
		foreach (var robot in robots)
		{
			DestroyRobot(robot);
		}

		PlayExplosionSoundIfNeeded(robots.Length > 0);
	}

	private void DestroyRobot(Robot robot)
	{
		if(robot != null && robot.TryGetComponent(out EntityExploder entityExploder))
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