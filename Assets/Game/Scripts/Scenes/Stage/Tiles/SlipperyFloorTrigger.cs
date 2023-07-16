using UnityEngine;

public class SlipperyFloorTrigger : MonoBehaviour, ITriggerable, IReversibleTrigger
{
	public void TriggerEffect(GameObject sender)
	{
		PlayerRobotMovement movement = sender.GetComponent<PlayerRobotMovement>();

		if(movement != null)
		{
			movement.IsSliding = true;
		}
	}

	public void ReverseTriggerEffect(GameObject sender)
	{
		PlayerRobotMovement movement = sender.GetComponent<PlayerRobotMovement>();

		if(movement != null)
		{
			movement.IsSliding = false;
		}
	}
}