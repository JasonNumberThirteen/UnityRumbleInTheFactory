using UnityEngine;

public class IndestructibleObjectTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerEffect(GameObject sender)
	{
		if(sender.tag.Contains("Player"))
		{
			StageManager.instance.audioManager.PlayPlayerRobotBulletHitSound();
		}
	}
}