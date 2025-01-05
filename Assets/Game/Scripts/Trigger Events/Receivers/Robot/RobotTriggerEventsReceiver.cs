using UnityEngine;

public class RobotTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private RobotHealth robotHealth;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out BulletEntity bulletEntity))
		{
			robotHealth.TakeDamage(bulletEntity.GetParent(), bulletEntity.GetDamage());
		}
	}
	
	protected virtual void Awake()
	{
		robotHealth = GetComponent<RobotHealth>();
	}
}