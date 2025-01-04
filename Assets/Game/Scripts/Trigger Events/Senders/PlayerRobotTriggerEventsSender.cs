using UnityEngine;

public class PlayerRobotTriggerEventsSender : RobotTriggerEventsReceiver
{
	private RobotShield robotShield;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(robotShield == null || !robotShield.IsActive())
		{
			base.TriggerOnEnter(sender);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		robotShield = GetComponentInChildren<RobotShield>();
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
}