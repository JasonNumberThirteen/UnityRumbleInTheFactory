using UnityEngine;

public class RobotEntityTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private RobotEntityHealth robotEntityHealth;
	private RobotEntityShield robotEntityShield;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(!ShieldIsActive() && sender.TryGetComponent(out BulletEntity bulletEntity))
		{
			robotEntityHealth.TakeDamage(bulletEntity.GetParentGO(), bulletEntity.GetDamage());
		}
	}
	
	protected virtual void Awake()
	{
		robotEntityHealth = GetComponent<RobotEntityHealth>();
		robotEntityShield = GetComponentInChildren<RobotEntityShield>();
	}

	protected bool ShieldIsActive() => robotEntityShield != null && robotEntityShield.IsActive();
}