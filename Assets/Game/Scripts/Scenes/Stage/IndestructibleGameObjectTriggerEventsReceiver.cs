using UnityEngine;

public class IndestructibleGameObjectTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		if(sender != null && sender.TryGetComponent(out PlayerRobotBullet _))
		{
			StageManager.instance.audioManager.PlayPlayerRobotBulletHitSound();
		}
	}
}