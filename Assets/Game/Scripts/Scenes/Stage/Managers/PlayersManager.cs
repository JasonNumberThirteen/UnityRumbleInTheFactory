using UnityEngine;

public class PlayersManager : MonoBehaviour
{
	[SerializeField] private PlayersListData playersListData;

	private readonly string PLAYER_TAG = "Player";

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
		var players = GameObject.FindGameObjectsWithTag(PLAYER_TAG);

		foreach (var player in players)
		{
			if(player.TryGetComponent(out PlayerRobotDisabler playerRobotDisabler))
			{
				playerRobotDisabler.DisableYourself();
			}
		}
	}
}