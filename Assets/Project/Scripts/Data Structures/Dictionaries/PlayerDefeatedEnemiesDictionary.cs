using System.Linq;
using System.Collections.Generic;

public class PlayerDefeatedEnemiesDictionary : Dictionary<EnemyRobotData, int>
{
	public int GetSumOfDefeatedEnemies() => Values.Sum();
	public int GetUnitsOf(EnemyRobotData enemyRobotData) => TryGetValue(enemyRobotData, out var numberOfDefeatedEnemies) ? numberOfDefeatedEnemies : 0;
	public List<KeyValuePair<EnemyRobotData, int>> GetPairsOrderedByOrdinalNumber() => this.OrderBy(pair => pair.Key.GetOrdinalNumber()).ToList();

	public void AddEnemyData(KeyValuePair<EnemyRobotData, int> keyValuePair)
	{
		AddEnemyData(keyValuePair.Key, keyValuePair.Value);
	}
	
	public void AddEnemyData(EnemyRobotData enemyRobotData, int units = 1)
	{
		if(TryGetValue(enemyRobotData, out var _))
		{
			this[enemyRobotData] += units;
		}
		else
		{
			Add(enemyRobotData, units);
		}
	}
}