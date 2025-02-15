using UnityEngine;

public class EnemyRobotEntityBulletEntityTriggerEventsSender : BulletEntityTriggerEventsSender
{
	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		if(!collider.TryGetComponent(out EnemyRobotEntityBulletEntityTriggerEventsSender _))
		{
			base.OnTriggerEnter2D(collider);
		}
	}
}