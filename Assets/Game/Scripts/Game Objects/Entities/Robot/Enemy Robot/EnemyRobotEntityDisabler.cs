using UnityEngine;

public class EnemyRobotEntityDisabler : RobotEntityDisabler
{
	private void Awake()
	{
		var robotEntitiesDisablingManager = FindAnyObjectByType<RobotEntitiesDisablingManager>(FindObjectsInactive.Include);

		if(robotEntitiesDisablingManager != null && robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled(false))
		{
			SetBehavioursActive(false);
		}
	}
}