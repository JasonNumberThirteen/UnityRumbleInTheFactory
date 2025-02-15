using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayersDefeatedEnemiesSumContainer : MonoBehaviour
{
	public PlayerDefeatedEnemiesDictionary TotalDefeatedEnemies {get; private set;} = new();
	
	[SerializeField] private PlayerRobotsListData playerRobotsListData;
	
	private void Awake()
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.GetPlayerRobotsData().ForEach(AddDefeatedEnemiesFromSinglePlayerIfPossible);
		}
	}

	private void AddDefeatedEnemiesFromSinglePlayerIfPossible(PlayerRobotData playerRobotData)
	{
		if(playerRobotData == null)
		{
			return;
		}

		var pairsOrderedByOrdinalNumber = playerRobotData.DefeatedEnemies.GetPairsOrderedByOrdinalNumber();

		pairsOrderedByOrdinalNumber.ForEach(TotalDefeatedEnemies.AddEnemyData);
	}
}