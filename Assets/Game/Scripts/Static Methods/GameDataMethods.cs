public static class GameDataMethods
{
	public static bool AnyStageFound(GameData gameData) => gameData != null && gameData.AnyStageFound();

	public static int[] GetTileIndexesFromCurrentStageData(GameData gameData)
	{
		if(gameData == null)
		{
			return new int[0];
		}
		
		var currentStageData = gameData.GetCurrentStageData();

		return currentStageData != null ? currentStageData.GetTileIndexes() : new int[0];
	}
}