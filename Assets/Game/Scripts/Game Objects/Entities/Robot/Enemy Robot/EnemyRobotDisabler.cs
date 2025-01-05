using UnityEngine;

public class EnemyRobotDisabler : RobotEntityDisabler
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