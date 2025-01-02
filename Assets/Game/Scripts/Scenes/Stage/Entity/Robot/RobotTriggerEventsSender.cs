using UnityEngine;

public class RobotTriggerEventsSender : MonoBehaviour, ITriggerableOnEnter
{
	private RobotHealth robotHealth;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out Bullet bulletStats))
		{
			robotHealth.TakeDamage(bulletStats.GetParent(), bulletStats.GetDamage());
		}
	}
	
	protected virtual void Awake()
	{
		robotHealth = GetComponent<RobotHealth>();
	}
}