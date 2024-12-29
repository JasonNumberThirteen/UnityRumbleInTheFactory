using UnityEngine;

public class PlayerRobotTrigger : RobotTrigger
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(!ShieldIsActive())
		{
			base.TriggerOnEnter(sender);
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
		if(collider.gameObject.TryGetComponent(out ITriggerableOnExit rt))
		{
			rt.ReverseTriggerEffect(gameObject);
		}
	}
}