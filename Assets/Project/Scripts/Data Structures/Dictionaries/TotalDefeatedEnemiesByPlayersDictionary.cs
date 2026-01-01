using System.Linq;
using System.Collections.Generic;

public class TotalDefeatedEnemiesByPlayersDictionary : Dictionary<PlayerRobotData, int>
{
	public TotalDefeatedEnemiesByPlayersDictionary(PlayerRobotsListData playerRobotsListData)
	{
		if(playerRobotsListData == null)
		{
			return;
		}

		var alivePlayers = playerRobotsListData.GetPlayerRobotsData().Where(playerRobotData => playerRobotData.IsAlive).ToArray();

		alivePlayers.ForEach(alivePlayer => Add(alivePlayer, alivePlayer.DefeatedEnemies.GetSumOfDefeatedEnemies()));
	}
	
	public PlayerRobotData GetPlayerWithHighestNumberOfDefeatedEnemies() => this.Aggregate((a, b) => a.Value > b.Value ? a : b).Key;
}