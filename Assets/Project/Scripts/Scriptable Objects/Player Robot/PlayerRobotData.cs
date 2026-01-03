using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Robot Data")]
public class PlayerRobotData : RobotData
{
	public SaveablePlayerData saveablePlayerData;
	
	public PlayerDefeatedEnemiesDictionary DefeatedEnemies {get; private set;} = new();
	public PlayerRobotEntitySpawner Spawner {get; set;}

	public int CurrentHealth {get; set;}
	public bool IsAlive {get; set;}
	public bool WasAliveOnCurrentStage {get; set;}
	
	public int Score
	{
		get => saveablePlayerData.score;
		set => saveablePlayerData.score = value.GetClampedValue(0, int.MaxValue);
	}

	public int Lives
	{
		get => lives;
		set => lives = value.GetClampedValue(0, maxLives);
	}

	public int RankNumber
	{
		get => rankNumber;
		set => rankNumber = value.GetClampedValue(1, GetNumberOfRanks());
	}
	
	[SerializeField] private PlayerRobotRank[] ranks;
	[SerializeField, Min(0)] private int initialLives = 2;
	[SerializeField, Min(0)] private int maxLives = 9;
	[SerializeField, Min(1)] private int initialRankNumber = 1;

	private int lives;
	private int rankNumber;

	public override RobotRank GetRankByIndex(int index) => ranks.GetElementAt(index);
	public override int GetNumberOfRanks() => ranks != null && ranks.Length > 0 ? ranks.Length : 1;

	public void InitSaveablePlayerData()
	{
		ResetSaveablePlayerData();
	}

	[ContextMenu("Reset saveable player data")]
	public void ResetSaveablePlayerData()
	{
		saveablePlayerData.ResetData();
	}

	public void ResetData(bool isActive)
	{
		lives = isActive ? initialLives : 0;
		IsAlive = WasAliveOnCurrentStage = isActive;
		
		saveablePlayerData.ResetData();
		ResetRank();
		ResetDefeatedEnemies();
		ResetCurrentHealth();
	}

	public void ResetRank()
	{
		RankNumber = initialRankNumber;
	}
	
	public void ResetDefeatedEnemies()
	{
		DefeatedEnemies.Clear();
	}

	public void AddDefeatedEnemy(EnemyRobotData enemyRobotData)
	{
		DefeatedEnemies.AddEnemyData(enemyRobotData);
	}

	private void ResetCurrentHealth()
	{
		var rank = GetRankByIndex(rankNumber - 1);

		if(rank != null)
		{
			CurrentHealth = rank.GetHealth();
		}
	}
}