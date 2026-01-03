using UnityEngine;

public class PlayerRobotEntityTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private PlayerRobotEntityFriendlyFireController playerRobotEntityFriendlyFireController;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotEntityBulletEntity playerRobotEntityBulletEntity) && playerRobotEntityBulletEntity.GetParentGO() != gameObject)
		{
			ImmobiliseByFriendlyFireIfPossible();
		}
		else
		{
			base.TriggerOnEnter(sender);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityFriendlyFireController = GetComponentInChildren<PlayerRobotEntityFriendlyFireController>();
	}

	private void ImmobiliseByFriendlyFireIfPossible()
	{
		if(playerRobotEntityFriendlyFireController != null && !ShieldIsActive())
		{
			playerRobotEntityFriendlyFireController.ImmobiliseTemporarily();
		}
	}
}