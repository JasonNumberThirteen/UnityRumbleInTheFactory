using UnityEngine;

public class EnemyRobotDisabler : RobotDisabler
{
	private void Awake()
	{
		var robotEntitiesDisablingManager = FindAnyObjectByType<RobotEntitiesDisablingManager>(FindObjectsInactive.Include);

		if(robotEntitiesDisablingManager != null && robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled())
		{
			SetBehavioursActive(false);
		}
	}
}