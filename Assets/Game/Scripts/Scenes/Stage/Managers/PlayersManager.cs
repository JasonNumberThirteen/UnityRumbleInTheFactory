using UnityEngine;

public class PlayersManager : MonoBehaviour
{
	public string playerTag, playerSpawnerTag;
	
	private PlayerData[] playersData;

	public void ResetDefeatedEnemiesByPlayer()
	{
		foreach (PlayerData pd in playersData)
		{
			pd.ResetDefeatedEnemies();
		}
	}

	public void CheckPlayersLives()
	{
		if(AllPlayersLostAllLives())
		{
			StageManager.instance.SetGameAsOver();
		}
	}

	public void DisablePlayers()
	{
		GameObject[] players = FoundObjectsWithTag(playerTag);

		foreach (GameObject player in players)
		{
			if(player.TryGetComponent(out PlayerRobotDisabler prd))
			{
				prd.DisableYourself();
			}
		}
	}

	public void FindPlayers()
	{
		GameObject[] spawners = FoundObjectsWithTag(playerSpawnerTag);
		int length = spawners.Length;

		playersData = new PlayerData[length];

		for (int i = 0; i < length; ++i)
		{
			if(spawners[i].TryGetComponent(out PlayerEntitySpawner ps))
			{
				playersData[i] = ps.GetPlayerData();
			}
		}
	}

	private GameObject[] FoundObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);

	private bool AllPlayersLostAllLives()
	{
		foreach (PlayerData pd in playersData)
		{
			if(pd.Lives > 0)
			{
				return false;
			}
		}

		return true;
	}
}