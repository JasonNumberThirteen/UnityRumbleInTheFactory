using UnityEngine;

public class PlayerRobotTriggerEventsReceiver : RobotTriggerEventsReceiver
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
}