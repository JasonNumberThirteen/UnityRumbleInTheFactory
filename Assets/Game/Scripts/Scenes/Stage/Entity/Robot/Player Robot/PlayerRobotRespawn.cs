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

			data.Rank = 1;
			timer.ResetTimer();
		}
		else
		{
			StageManager.instance.SetGameAsOver();
		}
	}
}