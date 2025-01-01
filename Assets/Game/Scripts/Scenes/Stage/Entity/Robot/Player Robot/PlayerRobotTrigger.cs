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

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerableOnEnter triggerableOnEnter))
		{
			triggerableOnEnter.TriggerOnEnter(gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerableOnStay triggerableOnStay))
		{
			triggerableOnStay.TriggerOnStay(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerableOnExit triggerableOnExit))
		{
			triggerableOnExit.TriggerOnExit(gameObject);
		}
	}

	private bool ShieldIsActive() => TryGetComponent(out RobotShield playerRobotShield) && playerRobotShield.ShieldTimer.gameObject.activeInHierarchy;
}