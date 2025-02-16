using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRobotEntityShootController : RobotEntityShootController
{
	private int numberOfFiredBullets;
	private int bulletsLimitAtOnce;
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
			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.AddListener(OnEventReceived);
			}
		}
		else
		{
			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.RemoveListener(OnEventReceived);
			}
		}
	}

	private void OnEventReceived(StageEventType stageEventType, GameObject sender)
	{
		if(stageEventType == StageEventType.BulletDestroyed && sender.TryGetComponent(out PlayerRobotEntityBulletEntity playerRobotEntityBulletEntity) && playerRobotEntityBulletEntity.GetParentGO() == gameObject)
		{
			--numberOfFiredBullets;
		}
	}

	private void OnFire(InputValue inputValue)
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