using UnityEngine;

public class RobotTrigger : MonoBehaviour, ITriggerable
{
	protected RobotHealth health;
	
	public virtual void TriggerEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out BulletStats bs))
		{
			health.TakeDamage(bs.parent, bs.damage);
		}
	}
	
	protected virtual void Awake() => health = GetComponent<RobotHealth>();
}