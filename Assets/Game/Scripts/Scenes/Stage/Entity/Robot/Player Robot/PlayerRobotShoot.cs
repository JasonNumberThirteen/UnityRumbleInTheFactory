using UnityEngine;

public class PlayerRobotShoot : RobotShoot
{
	public string bulletTag;
	
	private PlayerRobotRank rank;

	public override void FireBullet()
	{
		if(!FiredAllBulletsAlready())
		{
			InstantiateBullet();
		}
	}

	protected override void Awake()
	{
		base.Awake();

		rank = GetComponent<PlayerRobotRank>();
	}

	private bool FiredAllBulletsAlready() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= rank.CurrentRank.bulletLimit;
	private Vector2 BulletPosition() => (Vector2)transform.position + BulletDirection()*offsetFromObject;

	private void InstantiateBullet()
	{
		GameObject instance = Instantiate(bullet, BulletPosition(), Quaternion.identity);
		
		SetMovementDirectionToBullet(instance);
		SetStatsToBullet(instance);
	}

	private void SetMovementDirectionToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out EntityMovement em))
		{
			em.Direction = BulletDirection();
		}
	}

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