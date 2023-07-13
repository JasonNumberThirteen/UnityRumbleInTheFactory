using UnityEngine;

public class EnemyRobotShoot : RobotShoot
{
	[Min(0.01f)] public float fireDelay = 1f;

	private EnemyRobotFreeze freeze;
	
	public override void FireBullet()
	{
		if(freeze.Frozen)
		{
			return;
		}
		
		base.FireBullet();
	}

	protected override void Awake()
	{
		base.Awake();

		freeze = GetComponent<EnemyRobotFreeze>();
	}

	private void Start() => InvokeRepeating("FireBullet", fireDelay, fireDelay);
}