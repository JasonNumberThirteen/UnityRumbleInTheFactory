using System.Linq;
using UnityEngine;

public class DestructionBonusTrigger : BonusTrigger
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

		PlayExplosionSoundIfNeeded(robots);
	}

	private void DestroyRobot(Robot robot)
	{
		if(robot == null || !robot.TryGetComponent(out EntityExploder entityExploder))
		{
			return;
		}
		
		entityExploder.TriggerExplosion();

		if(!robot.IsFriendly())
		{
			StageManager.instance.CountDefeatedEnemy();
		}
	}

	private void PlayExplosionSoundIfNeeded(Robot[] robots)
	{
		if(stageSoundManager != null && robots.Length > 0)
		{
			stageSoundManager.PlaySound(SoundEffectType.EnemyRobotExplosion);
		}
	}
}