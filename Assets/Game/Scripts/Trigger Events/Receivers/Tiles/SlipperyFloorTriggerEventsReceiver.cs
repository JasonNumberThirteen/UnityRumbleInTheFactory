using UnityEngine;

public class SlipperyFloorTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter, ITriggerableOnExit
{
	public void TriggerOnEnter(GameObject sender)
	{
		SetSlidingToPlayerRobotIfPossible(sender, true);
	}

	public void TriggerOnExit(GameObject sender)
	{
		SetSlidingToPlayerRobotIfPossible(sender, false);
	}

	private void SetSlidingToPlayerRobotIfPossible(GameObject sender, bool isSliding)
	{
		if(sender.TryGetComponent(out PlayerRobotEntityMovement playerRobotEntityMovement))
		{
			playerRobotEntityMovement.IsSliding = isSliding;
		}
	}
}