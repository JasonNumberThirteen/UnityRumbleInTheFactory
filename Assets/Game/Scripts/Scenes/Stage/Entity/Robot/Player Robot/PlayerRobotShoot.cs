using UnityEngine;

public class PlayerRobotShoot : RobotShoot
{
	public string bulletTag;
	
	private PlayerRobotRankController rank;

	public override void FireBullet()
	{
		if(!FiredAllBulletsAlready())
		{
			if(stageSoundManager != null)
			{
				stageSoundManager.PlaySound(SoundEffectType.PlayerRobotShoot);
			}
			
			base.FireBullet();
		}
	}

	protected override void Awake()
	{
		base.Awake();

		rank = GetComponent<PlayerRobotRankController>();
	}

	protected override void SetBullet(GameObject bullet)
	{
		base.SetBullet(bullet);
		SetStatsToBullet(bullet);
	}

	private bool FiredAllBulletsAlready() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= rank.CurrentRank.GetBulletsLimitAtOnce();

	private void SetStatsToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out Bullet bs))
		{
			bs.SetDamage(rank.CurrentRank.GetDamage());
			bs.SetMovementSpeed(rank.CurrentRank.GetBulletSpeed());
			bs.SetCanDestroyMetal(rank.CurrentRank.CanDestroyMetal());
		}
	}
}