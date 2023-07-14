using UnityEngine;

public class PlayerRobotRespawn : MonoBehaviour
{
	public PlayerData data;
	public string spawnerTag;

	public void Respawn()
	{
		if(data.Lives-- > 0)
		{
			GameObject spawner = GameObject.FindGameObjectWithTag(spawnerTag);
			Timer timer = spawner.GetComponent<Timer>();

			timer.ResetTimer();
		}
		else
		{
			StageManager.instance.SetGameAsOver();
		}

		StageManager.instance.uiManager.UpdateCounters();
	}
}