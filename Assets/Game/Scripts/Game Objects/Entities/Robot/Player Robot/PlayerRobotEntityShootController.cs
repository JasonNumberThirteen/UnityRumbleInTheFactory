using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityRankController))]
public class PlayerRobotEntityShootController : RobotEntityShootController
{
	[SerializeField] private string bulletTag;
	
	private PlayerRobotEntityRankController playerRobotEntityRankController;

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
	}

	protected override void SetupBulletEntity(BulletEntity bulletEntity)
	{
		base.SetupBulletEntity(bulletEntity);

		if(playerRobotEntityRankController.CurrentRank == null || bulletEntity == null)
		{
			return;
		}

		bulletEntity.SetDamage(playerRobotEntityRankController.CurrentRank.GetDamage());
		bulletEntity.SetMovementSpeed(playerRobotEntityRankController.CurrentRank.GetBulletSpeed());
		bulletEntity.SetCanDestroyMetal(playerRobotEntityRankController.CurrentRank.CanDestroyMetal());
	}

	private bool ReachedBulletsLimitAtOnce()
	{
		if(playerRobotEntityRankController.CurrentRank is not PlayerRobotRank playerRobotRank)
		{
			return false;
		}

		return GameObject.FindGameObjectsWithTag(bulletTag).Length >= playerRobotRank.GetBulletsLimitAtOnce();
	}
}