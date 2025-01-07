using UnityEngine;

public class SlipperyFloorTileTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter, ITriggerableOnStay, ITriggerableOnExit
{
	public void TriggerOnEnter(GameObject sender)
	{
		SetSlidingToPlayerRobotIfPossible(sender, true);
	}

	public void TriggerOnStay(GameObject sender)
	{
		SetSlidingToPlayerRobotIfPossible(sender, true);
	}

	public void TriggerOnExit(GameObject sender)
	{
		SetSlidingToPlayerRobotIfPossible(sender, false);
	}

	private void SetSlidingToPlayerRobotIfPossible(GameObject sender, bool isSliding)
	{
		if(sender.TryGetComponent(out PlayerRobotEntityMovementController playerRobotEntityMovementController))
		{
			playerRobotEntityMovementController.IsSliding = isSliding;
		}
	}
}