using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityRankController))]
public class PlayerRobotEntityShootController : RobotEntityShootController
{
	[SerializeField] private string bulletTag;
	
	private PlayerRobotEntityRankController playerRobotEntityRankController;
	private BulletStats bulletStats;
	private int bulletsLimitAtOnce;

	public override void FireBullet()
	{
		if(ReachedBulletsLimitAtOnce())
		{
			return;
		}
		
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotShoot);
		}
		
		base.FireBullet();
	}

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityRankController = GetComponent<PlayerRobotEntityRankController>();

		RegisterToListeners(true);
	}

	protected override void SetupBulletEntity(BulletEntity bulletEntity)
	{
		base.SetupBulletEntity(bulletEntity);
		bulletEntity.SetStats(bulletStats);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			playerRobotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			playerRobotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnRankChanged(RobotRank robotRank)
	{
		bulletStats = new BulletStats(robotRank.GetDamage(), robotRank.GetBulletSpeed(), robotRank.CanDestroyMetal());

		if(robotRank is PlayerRobotRank playerRobotRank)
		{
			bulletsLimitAtOnce = playerRobotRank.GetBulletsLimitAtOnce();
		}
	}

	private bool ReachedBulletsLimitAtOnce() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= bulletsLimitAtOnce;
}