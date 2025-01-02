using UnityEngine;

[RequireComponent(typeof(PlayerRobotRankController))]
public class PlayerRobotShoot : RobotShoot
{
	[SerializeField] private string bulletTag;
	
	private PlayerRobotRankController playerRobotRankController;

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

		playerRobotRankController = GetComponent<PlayerRobotRankController>();
	}

	protected override void SetupBullet(GameObject bulletGO)
	{
		base.SetupBullet(bulletGO);
		SetupBulletIfPossible(bulletGO);
	}

	private void SetupBulletIfPossible(GameObject bulletGO)
	{
		if(playerRobotRankController.CurrentRank == null || !bulletGO.TryGetComponent(out Bullet bullet))
		{
			return;
		}

		bullet.SetDamage(playerRobotRankController.CurrentRank.GetDamage());
		bullet.SetMovementSpeed(playerRobotRankController.CurrentRank.GetBulletSpeed());
		bullet.SetCanDestroyMetal(playerRobotRankController.CurrentRank.CanDestroyMetal());
	}

	private bool ReachedBulletsLimitAtOnce() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= playerRobotRankController.CurrentRank.GetBulletsLimitAtOnce();
}