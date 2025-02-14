using UnityEngine;

public class EnemyRobotEntityTriggerEventsSender : RobotEntityTriggerEventsSender
{
	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerableOnExit triggerableOnExit))
		{
			triggerableOnExit.TriggerOnExit(gameObject);
		}
	}
}