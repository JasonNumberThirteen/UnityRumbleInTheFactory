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
		
		Vector2 position = transform.position;
		Vector2 bulletDirection = BulletDirection();
		GameObject instance = Instantiate(bullet, position + bulletDirection*offsetFromObject, Quaternion.identity);
		EntityMovement em = instance.GetComponent<EntityMovement>();
		BulletStats bs = instance.GetComponent<BulletStats>();

		if(em != null)
		{
			em.Direction = bulletDirection;
		}

		if(bs != null)
		{
			bs.damage = rank.CurrentRank.damage;
			bs.speed = rank.CurrentRank.bulletSpeed;
			bs.canDestroyMetal = rank.CurrentRank.canDestroyMetal;
		}
	}
}