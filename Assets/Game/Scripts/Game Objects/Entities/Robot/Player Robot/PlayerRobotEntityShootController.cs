using UnityEngine;
public class PlayerRobotEntityShootController : RobotEntityShootController
{
	private StageEventsManager stageEventsManager;
	private int numberOfFiredBullets;
	private int bulletsLimitAtOnce;

	public override void FireBullet()
	{
		if(ReachedBulletsLimitAtOnce())
		{
			return;
		}

		++numberOfFiredBullets;
		
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotShoot);
		}
		
		base.FireBullet();
	}

	protected override void Awake()
	{
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		
		base.Awake();
	}

	protected override void OnRankChanged(RobotRank robotRank)
	{
		base.OnRankChanged(robotRank);
		
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

	private bool ReachedBulletsLimitAtOnce() => numberOfFiredBullets >= bulletsLimitAtOnce;
}