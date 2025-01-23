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

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			if(enemyRobotEntityShootControllerTimer != null)
			{
				enemyRobotEntityShootControllerTimer.timerReachedEndEvent.AddListener(OnTimerReachedEnd);
			}
		}
		else
		{
			if(enemyRobotEntityShootControllerTimer != null)
			{
				enemyRobotEntityShootControllerTimer.timerReachedEndEvent.RemoveListener(OnTimerReachedEnd);
			}
		}
	}

	private void OnTimerReachedEnd()
	{
		FireBullet();
		enemyRobotEntityShootControllerTimer.ResetTimer();
	}
}