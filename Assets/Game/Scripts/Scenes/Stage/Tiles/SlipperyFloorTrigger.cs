using UnityEngine;

public class SlipperyFloorTrigger : MonoBehaviour, ITriggerableOnEnter, ITriggerableOnExit
{
	public void TriggerOnEnter(GameObject sender)
	{
		SetSliding(sender, true);
	}

	public void ReverseTriggerEffect(GameObject sender)
	{
		SetSliding(sender, false);
	}

	private void SetSliding(GameObject sender, bool isSliding)
	{
		if(sender.TryGetComponent(out PlayerRobotMovement playerRobotMovement))
		{
			playerRobotMovement.IsSliding = isSliding;
		}
	}
}