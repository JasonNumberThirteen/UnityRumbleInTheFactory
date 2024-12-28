using UnityEngine;

public class PlayersManager : MonoBehaviour
{
	public string playerTag, playerSpawnerTag;
	public Timer gameOverTimer;
	
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
			gameOverTimer.onEnd.Invoke();
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
			if(spawners[i].TryGetComponent(out PlayerSpawner ps))
			{
				playersData[i] = ps.playerData;
			}
		}
	}

	private GameObject[] FoundObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);

	private bool AllPlayersLostAllLives()
	{
		foreach (PlayerData pd in playersData)
		{
			if(!pd.LostAllLives)
			{
				return false;
			}
		}

		return true;
	}
}