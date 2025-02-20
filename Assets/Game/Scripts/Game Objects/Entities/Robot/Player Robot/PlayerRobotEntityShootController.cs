using UnityEngine;

public class PlayerRobotEntityShootController : RobotEntityShootController
{
	private int numberOfFiredBullets;
	private int bulletsLimitAtOnce;
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private StageStateManager stageStateManager;
	private StageEventsManager stageEventsManager;

	protected override void FireBullet()
	{
		if(ReachedBulletsLimitAtOnce())
		{
			return;
		}

		++numberOfFiredBullets;
		
		PlaySound();
		base.FireBullet();
	}

	protected override void Awake()
	{
		playerRobotEntityInputController = GetComponent<PlayerRobotEntityInputController>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		
		base.Awake();
	}

	protected override void OnRankChanged(RobotRank robotRank, bool setOnStart)
	{
		base.OnRankChanged(robotRank, setOnStart);
		
		if(robotRank != null && robotRank is PlayerRobotRank playerRobotRank)
		{
			bulletsLimitAtOnce = playerRobotRank.GetBulletsLimitAtOnce();
		}
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			playerRobotEntityInputController.shootKeyPressedEvent.AddListener(OnShootKeyPressed);
			
			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.AddListener(OnEventReceived);
			}
		}
		else
		{
			playerRobotEntityInputController.shootKeyPressedEvent.RemoveListener(OnShootKeyPressed);
			
			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.RemoveListener(OnEventReceived);
			}
		}
	}

	private void OnEventReceived(StageEventType stageEventType, GameObject sender)
	{
		if(stageEventType == StageEventType.BulletWasDestroyed && sender.TryGetComponent(out PlayerRobotEntityBulletEntity playerRobotEntityBulletEntity) && playerRobotEntityBulletEntity.GetParentGO() == gameObject)
		{
			--numberOfFiredBullets;
		}
	}

	private void OnShootKeyPressed()
	{
		if(enabled && !GameIsPaused())
		{
			FireBullet();
		}
	}

	private void PlaySound()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotShoot);
		}
	}

	private bool ReachedBulletsLimitAtOnce() => numberOfFiredBullets >= bulletsLimitAtOnce;
	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
}