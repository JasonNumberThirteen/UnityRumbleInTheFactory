using UnityEngine;

public class RobotTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private RobotEntityHealth robotEntityHealth;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out BulletEntity bulletEntity))
		{
			robotEntityHealth.TakeDamage(bulletEntity.GetParent(), bulletEntity.GetDamage());
		}
	}
	
	protected virtual void Awake()
	{
		robotEntityHealth = GetComponent<RobotEntityHealth>();
	}
}