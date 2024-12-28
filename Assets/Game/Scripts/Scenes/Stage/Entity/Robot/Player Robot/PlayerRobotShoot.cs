using UnityEngine;

public class PlayerRobotShoot : RobotShoot
{
	public string bulletTag;
	
	private PlayerRobotRankController rank;

	public override void FireBullet()
	{
		if(!FiredAllBulletsAlready())
		{
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

	private bool FiredAllBulletsAlready() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= rank.CurrentRank.bulletLimit;

	private void SetStatsToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out BulletStats bs))
		{
			bs.damage = rank.CurrentRank.damage;
			bs.speed = rank.CurrentRank.bulletSpeed;
			bs.canDestroyMetal = rank.CurrentRank.canDestroyMetal;
		}
	}
}