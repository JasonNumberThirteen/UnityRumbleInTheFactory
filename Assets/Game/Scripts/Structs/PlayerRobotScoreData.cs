public readonly struct PlayerRobotScoreData
{
	public PlayerRobotData PlayerRobotData {get;}
	public int NumberOfCountedEnemyRobots {get;}
	public int CurrentScoreForDefeatedEnemyRobots {get;}

	public PlayerRobotScoreData(PlayerRobotData playerRobotData, int numberOfCountedEnemyRobots, int currentScoreForDefeatedEnemyRobots)
	{
		PlayerRobotData = playerRobotData;
		NumberOfCountedEnemyRobots = numberOfCountedEnemyRobots;
		CurrentScoreForDefeatedEnemyRobots = currentScoreForDefeatedEnemyRobots;
	}
}