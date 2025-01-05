public class EnemyRobotEntityShoot : RobotEntityShoot
{
	private EnemyRobotEntityShootTimer enemyRobotEntityShootTimer;

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityShootTimer = GetComponentInChildren<EnemyRobotEntityShootTimer>();

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
			if(enemyRobotEntityShootTimer != null)
			{
				enemyRobotEntityShootTimer.onEnd.AddListener(OnTimerEnd);
			}
		}
		else
		{
			if(enemyRobotEntityShootTimer != null)
			{
				enemyRobotEntityShootTimer.onEnd.RemoveListener(OnTimerEnd);
			}
		}
	}

	private void OnTimerEnd()
	{
		FireBullet();
		enemyRobotEntityShootTimer.ResetTimer();
	}
}