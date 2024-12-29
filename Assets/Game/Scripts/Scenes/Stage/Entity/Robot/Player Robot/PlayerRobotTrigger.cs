using UnityEngine;

public class PlayerRobotTrigger : RobotTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(!ShieldIsActive())
		{
			base.TriggerEffect(sender);
		}
	}

	private bool ShieldIsActive() => TryGetComponent(out PlayerRobotShield prs) && prs.ShieldTimer.gameObject.activeInHierarchy;

	private void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerableOnStay triggerableOnStay))
		{
			triggerableOnStay.TriggerOnStay(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out IReversibleTrigger rt))
		{
			rt.ReverseTriggerEffect(gameObject);
		}
	}
}