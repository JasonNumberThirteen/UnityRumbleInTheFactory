using UnityEngine;

public class PlayerRobotEntityTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private RobotEntityShield robotEntityShield;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(robotEntityShield == null || !robotEntityShield.IsActive())
		{
			base.TriggerOnEnter(sender);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		robotEntityShield = GetComponentInChildren<RobotEntityShield>();
	}
}