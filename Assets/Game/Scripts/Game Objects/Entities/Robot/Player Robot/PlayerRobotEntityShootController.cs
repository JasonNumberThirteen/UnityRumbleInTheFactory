using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInputTypesActivationController))]
public class PlayerRobotEntityShootController : RobotEntityShootController
{
	private int numberOfFiredBullets;
	private int bulletsLimitAtOnce;
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private PlayerRobotEntityInputTypesActivationController playerRobotEntityInputTypesActivationController;
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
		playerRobotEntityInputTypesActivationController = GetComponent<PlayerRobotEntityInputTypesActivationController>();
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
			playerRobotEntityInputController.shootKeyWasPressedEvent.AddListener(OnShootKeyPressed);
			
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.AddListener(OnEventReceived);
			}
		}
		else
		{
			playerRobotEntityInputController.shootKeyWasPressedEvent.RemoveListener(OnShootKeyPressed);
			
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.RemoveListener(OnEventReceived);
			}
		}
	}

	private void OnEventReceived(StageEvent stageEvent)
	{
		if(stageEvent.GetStageEventType() != StageEventType.BulletWasDestroyed || stageEvent is not GameObjectStageEvent gameObjectStageEvent)
		{
			return;
		}

		var go = gameObjectStageEvent.GetGO();
		
		if(go != null && go.TryGetComponent(out PlayerRobotEntityBulletEntity playerRobotEntityBulletEntity) && playerRobotEntityBulletEntity.GetParentGO() == gameObject)
		{
			--numberOfFiredBullets;
		}
	}

	private void OnShootKeyPressed()
	{
		if(CanPerformInputActionOfType(PlayerInputActionType.Shoot) && !GameIsPaused())
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
	private bool CanPerformInputActionOfType(PlayerInputActionType playerInputActionType) => playerRobotEntityInputTypesActivationController == null || playerRobotEntityInputTypesActivationController.PlayerCanPerformInputActionOfType(playerInputActionType);
}