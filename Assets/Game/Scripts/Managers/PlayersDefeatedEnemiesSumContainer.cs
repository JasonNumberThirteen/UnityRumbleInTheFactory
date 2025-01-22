using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayersDefeatedEnemiesSumContainer : MonoBehaviour
{
	public Dictionary<EnemyRobotData, int> DefeatedEnemies {get; private set;} = new();
	
	[SerializeField] private PlayerRobotsListData playerRobotsListData;
	
	private void Awake()
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEach(AddDefeatedEnemiesFromSinglePlayerIfPossible);
		}
	}

	private void AddDefeatedEnemiesFromSinglePlayerIfPossible(PlayerRobotData playerRobotData)
	{
		if(playerRobotData != null)
		{
			playerRobotData.DefeatedEnemies.ToList().ForEach(AddNumberOfDefeatedEnemiesOfOneType);
		}
	}

	private void AddNumberOfDefeatedEnemiesOfOneType(KeyValuePair<EnemyRobotData, int> keyValuePair)
	{
		var key = keyValuePair.Key;
		var value = keyValuePair.Value;
		
		if(DefeatedEnemies.ContainsKey(key))
		{
			DefeatedEnemies[key] += value;
		}
		else
		{
			DefeatedEnemies[key] = value;
		}
	}
}