using UnityEngine;

public class PlayerRobotTrigger : RobotTrigger
{
	public PlayerData data;

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
		if(collider.gameObject.TryGetComponent(out ITriggerable triggerable))
		{
			triggerable.TriggerEffect(gameObject);
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