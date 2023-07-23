using UnityEngine;

public class PlayerRobotShoot : RobotShoot
{
	private PlayerRobotRank rank;

	protected override void Awake()
	{
		base.Awake();

		rank = GetComponent<PlayerRobotRank>();
	}
	
	public override void FireBullet()
	{
		if(GameObject.FindGameObjectsWithTag("Player Bullet").Length >= rank.CurrentRank.bulletLimit)
		{
			return;
		}
		
		GameObject instance = Instantiate(bullet, transform.position + BulletPositionOffset()*offsetFromObject, Quaternion.identity);
		EntityMovement em = instance.GetComponent<EntityMovement>();
		BulletStats bs = instance.GetComponent<BulletStats>();

		if(em != null)
		{
			em.Direction = BulletDirection();
		}

		if(bs != null)
		{
			bs.damage = rank.CurrentRank.damage;
			bs.speed = rank.CurrentRank.bulletSpeed;
			bs.canDestroyMetal = rank.CurrentRank.canDestroyMetal;
		}
	}
}