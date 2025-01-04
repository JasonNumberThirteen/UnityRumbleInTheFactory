using UnityEngine;

public class RobotTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	private RobotHealth robotHealth;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out Bullet bullet))
		{
			robotHealth.TakeDamage(bullet.GetParent(), bullet.GetDamage());
		}
	}
	
	protected virtual void Awake()
	{
		robotHealth = GetComponent<RobotHealth>();
	}
}