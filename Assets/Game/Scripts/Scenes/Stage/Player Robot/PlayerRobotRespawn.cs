using UnityEngine;

public class PlayerRobotRespawn : MonoBehaviour
{
	public PlayerData data;
	public string spawnerTag;

	public void Respawn()
	{
		if(--data.lives > 0)
		{
			GameObject spawner = GameObject.FindGameObjectWithTag(spawnerTag);
			Timer timer = spawner.GetComponent<Timer>();

			timer.ResetTimer();
		}
	}
}