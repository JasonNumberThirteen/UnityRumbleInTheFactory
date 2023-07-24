using UnityEngine;

public class RobotTrigger : MonoBehaviour, ITriggerable
{
	protected RobotHealth health;
	
	public virtual void TriggerEffect(GameObject sender)
	{
		BulletStats bs = sender.GetComponent<BulletStats>();

		if(bs != null)
		{
			health.CurrentHealth -= bs.damage;
		}
	}
	
	protected virtual void Awake() => health = GetComponent<RobotHealth>();
}