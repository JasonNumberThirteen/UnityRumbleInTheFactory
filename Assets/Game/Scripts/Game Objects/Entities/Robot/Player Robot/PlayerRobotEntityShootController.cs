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

	protected override void SetupBullet(GameObject bulletGO)
	{
		base.SetupBullet(bulletGO);
		SetupBulletIfPossible(bulletGO);
	}

	private void SetupBulletIfPossible(GameObject bulletGO)
	{
		if(playerRobotEntityRankController.CurrentRank == null || !bulletGO.TryGetComponent(out BulletEntity bulletEntity))
		{
			return;
		}

		bulletEntity.SetDamage(playerRobotEntityRankController.CurrentRank.GetDamage());
		bulletEntity.SetMovementSpeed(playerRobotEntityRankController.CurrentRank.GetBulletSpeed());
		bulletEntity.SetCanDestroyMetal(playerRobotEntityRankController.CurrentRank.CanDestroyMetal());
	}

	private bool ReachedBulletsLimitAtOnce() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= playerRobotEntityRankController.CurrentRank.GetBulletsLimitAtOnce();
}