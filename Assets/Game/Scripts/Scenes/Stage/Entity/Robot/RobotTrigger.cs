using UnityEngine;

public class RobotTrigger : MonoBehaviour, ITriggerable
{
	private RobotHealth health;
	
	public virtual void TriggerEffect() => health.ReceiveDamage(1);
	
	private void Awake() => health = GetComponent<RobotHealth>();
}