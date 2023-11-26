using UnityEngine;

public class IndestructibleObjectTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect(GameObject sender)
	{
		if(sender.tag.Contains("Player"))
		{
			StageManager.instance.audioManager.PlaySound(StageManager.instance.audioManager.playerRobotBulletHit);
		}
	}
}