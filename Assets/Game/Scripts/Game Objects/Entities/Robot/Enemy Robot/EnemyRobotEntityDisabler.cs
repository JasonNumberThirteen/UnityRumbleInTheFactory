public class EnemyRobotEntityDisabler : RobotEntityDisabler
{
	private void Awake()
	{
		var robotEntitiesDisablingManager = ObjectMethods.FindComponentOfType<RobotEntitiesDisablingManager>();

		if(robotEntitiesDisablingManager != null && robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled(false))
		{
			SetBehavioursActive(false);
		}
	}
}