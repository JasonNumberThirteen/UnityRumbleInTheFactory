using UnityEngine;

public class PlayersManager : MonoBehaviour
{
	public string playerTag, playerSpawnerTag;
	
	[SerializeField] private PlayersListData playersListData;

	public void ResetDefeatedEnemiesByPlayer()
	{
		if(playersListData != null)
		{
			playersListData.ForEach(playerData => playerData.ResetDefeatedEnemies());
		}
	}

	public void CheckPlayersLives()
	{
		if(playersListData != null && !playersListData.Any(playerData => playerData.Lives > 0))
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

	private GameObject[] FoundObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);
}