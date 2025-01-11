using UnityEngine;

public class RobotEntityTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private RobotEntityHealth robotEntityHealth;
	private RobotEntityShield robotEntityShield;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		var canTakeDamage = robotEntityShield == null || !robotEntityShield.IsActive();

		if(canTakeDamage && sender.TryGetComponent(out BulletEntity bulletEntity))
		{
			robotEntityHealth.TakeDamage(bulletEntity.GetParentGO(), bulletEntity.GetDamage());
		}
	}
	
	protected virtual void Awake()
	{
		robotEntityHealth = GetComponent<RobotEntityHealth>();
		robotEntityShield = GetComponentInChildren<RobotEntityShield>();
	}
}