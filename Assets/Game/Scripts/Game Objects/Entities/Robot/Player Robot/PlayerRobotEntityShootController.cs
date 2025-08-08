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

	protected override void OnRankWasChanged(RobotRank robotRank, bool setOnStart)
	{
		base.OnRankWasChanged(robotRank, setOnStart);
		
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
			playerRobotEntityInputController.shootKeyWasPressedEvent.AddListener(OnShootKeyWasPressed);
			
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.AddListener(OnEventWasSent);
			}
		}
		else
		{
			playerRobotEntityInputController.shootKeyWasPressedEvent.RemoveListener(OnShootKeyWasPressed);
			
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.RemoveListener(OnEventWasSent);
			}
		}
	}

	private void OnEventWasSent(StageEvent stageEvent)
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

	private void OnShootKeyWasPressed()
	{
		if(CanPerformInputActionOfType(PlayerInputActionType.Shoot) && !GameIsPaused() && !GameIsOver())
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
	private bool GameIsOver() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Over);
	private bool CanPerformInputActionOfType(PlayerInputActionType playerInputActionType) => playerRobotEntityInputTypesActivationController == null || playerRobotEntityInputTypesActivationController.PlayerCanPerformInputActionOfType(playerInputActionType);
}