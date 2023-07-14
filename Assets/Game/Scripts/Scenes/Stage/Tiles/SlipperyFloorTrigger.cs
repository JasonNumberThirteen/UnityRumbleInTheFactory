using UnityEngine;

public class SlipperyFloorTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect(GameObject sender)
	{
		PlayerRobotMovement movement = sender.GetComponent<PlayerRobotMovement>();

		if(movement != null)
		{
			movement.IsSliding = true;
		}
	}
}