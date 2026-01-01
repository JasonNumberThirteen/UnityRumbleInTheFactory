public class EnemyRobotEntityShootController : RobotEntityShootController
{
	private EnemyRobotEntityShootControllerTimer enemyRobotEntityShootControllerTimer;

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityShootControllerTimer = GetComponentInChildren<EnemyRobotEntityShootControllerTimer>();

		RegisterToListeners(true);
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			if(enemyRobotEntityShootControllerTimer != null)
			{
				enemyRobotEntityShootControllerTimer.timerFinishedEvent.AddListener(OnTimerFinished);
			}
		}
		else
		{
			if(enemyRobotEntityShootControllerTimer != null)
			{
				enemyRobotEntityShootControllerTimer.timerFinishedEvent.RemoveListener(OnTimerFinished);
			}
		}
	}

	private void OnTimerFinished()
	{
		FireBullet();

		if(enemyRobotEntityShootControllerTimer != null)
		{
			enemyRobotEntityShootControllerTimer.StartTimer();
		}
	}

	private void OnEnable()
	{
		SetTimerFrozenIfPossible(false);
	}

	private void OnDisable()
	{
		SetTimerFrozenIfPossible(true);
	}

	private void SetTimerFrozenIfPossible(bool freeze)
	{
		if(enemyRobotEntityShootControllerTimer != null)
		{
			enemyRobotEntityShootControllerTimer.SetTimeFrozen(freeze);
		}
	}
}