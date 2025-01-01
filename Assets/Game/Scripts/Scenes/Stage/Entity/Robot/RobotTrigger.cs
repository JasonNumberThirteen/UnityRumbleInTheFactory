using UnityEngine;

public class RobotTrigger : MonoBehaviour, ITriggerableOnEnter
{
	private RobotHealth robotHealth;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out BulletStats bulletStats))
		{
			robotHealth.TakeDamage(bulletStats.parent, bulletStats.damage);
		}
	}
	
	protected virtual void Awake()
	{
		robotHealth = GetComponent<RobotHealth>();
	}
}