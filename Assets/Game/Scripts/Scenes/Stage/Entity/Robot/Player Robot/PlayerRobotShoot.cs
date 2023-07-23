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
		
		base.FireBullet();
	}
}