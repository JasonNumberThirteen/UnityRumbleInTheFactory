using UnityEngine;

public class IndestructibleGameObjectTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		if(sender.tag.Contains("Player"))
		{
			StageManager.instance.audioManager.PlayPlayerRobotBulletHitSound();
		}
	}
}