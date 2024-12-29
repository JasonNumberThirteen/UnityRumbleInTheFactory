using UnityEngine;

public class IndestructibleObjectTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		if(sender.tag.Contains("Player"))
		{
			StageManager.instance.audioManager.PlayPlayerRobotBulletHitSound();
		}
	}
}