public class EnemyRobotEntityShootController : RobotEntityShootController
{
	private EnemyRobotEntityShootControllerTimer enemyRobotEntityShootControllerTimer;

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityShootControllerTimer = GetComponentInChildren<EnemyRobotEntityShootControllerTimer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(enemyRobotEntityShootControllerTimer != null)
			{
				enemyRobotEntityShootControllerTimer.onEnd.AddListener(OnTimerEnd);
			}
		}
		else
		{
			if(enemyRobotEntityShootControllerTimer != null)
			{
				enemyRobotEntityShootControllerTimer.onEnd.RemoveListener(OnTimerEnd);
			}
		}
	}

	private void OnTimerEnd()
	{
		FireBullet();
		enemyRobotEntityShootControllerTimer.ResetTimer();
	}
}