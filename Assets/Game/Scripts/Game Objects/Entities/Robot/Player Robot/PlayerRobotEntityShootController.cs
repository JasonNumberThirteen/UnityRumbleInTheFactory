using UnityEngine;
public class PlayerRobotEntityShootController : RobotEntityShootController
{
	[SerializeField] private string bulletTag;
	
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

	protected override void OnRankChanged(RobotRank robotRank)
	{
		base.OnRankChanged(robotRank);
		
		if(robotRank is PlayerRobotRank playerRobotRank)
		{
			bulletsLimitAtOnce = playerRobotRank.GetBulletsLimitAtOnce();
		}
	}

	private bool ReachedBulletsLimitAtOnce() => GameObject.FindGameObjectsWithTag(bulletTag).Length >= bulletsLimitAtOnce;
}