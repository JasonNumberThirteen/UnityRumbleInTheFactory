using UnityEngine;

public class PlayersManager : MonoBehaviour
{
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
}