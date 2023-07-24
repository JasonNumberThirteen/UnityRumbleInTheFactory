using UnityEngine;

public class RobotTrigger : MonoBehaviour, ITriggerable
{
	protected RobotHealth health;
	
	public virtual void TriggerEffect(GameObject sender) => health.CurrentHealth -= 1;
	
	protected virtual void Awake() => health = GetComponent<RobotHealth>();
}