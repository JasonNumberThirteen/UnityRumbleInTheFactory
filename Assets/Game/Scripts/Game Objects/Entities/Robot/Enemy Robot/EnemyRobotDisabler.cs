using UnityEngine;

public class EnemyRobotDisabler : RobotDisabler
{
	private void Awake()
	{
		var robotDisablingManager = FindAnyObjectByType<RobotDisablingManager>(FindObjectsInactive.Include);

		if(robotDisablingManager != null && robotDisablingManager.RobotsAreTemporarilyDisabled())
		{
			SetBehavioursActive(false);
		}
	}
}