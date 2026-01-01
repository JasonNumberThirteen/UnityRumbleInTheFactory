using System;

public static class GameDataMethods
{
	public static bool EnteredStageSelection(GameData gameData) => GameDataIsDefined(gameData) && gameData.EnteredStageSelection;
	public static bool SelectedTwoPlayerMode(GameData gameData) => GameDataIsDefined(gameData) && gameData.SelectedTwoPlayerMode;
	public static bool GameIsOver(GameData gameData) => GameDataIsDefined(gameData) && gameData.GameIsOver;
	public static bool BeatenHighScore(GameData gameData) => GameDataIsDefined(gameData) && gameData.BeatenHighScore;
	public static int GetCurrentDifficultyTierIndex(GameData gameData) => GameDataIsDefined(gameData) ? gameData.GetCurrentDifficultyTierIndex() : 0;
	public static T GetDifficultyTierValue<T>(GameData gameData, Func<GameDifficultyTier, T> tierFunc) where T : struct => GameDataIsDefined(gameData) ? gameData.GetDifficultyTierValue(tierFunc) : default;
	public static StageData GetCurrentStageData(GameData gameData) => GameDataIsDefined(gameData) ? gameData.GetCurrentStageData() : null;
	public static bool AnyStageFound(GameData gameData) => GameDataIsDefined(gameData) && gameData.AnyStageFound();
	public static bool GameDataIsDefined(GameData gameData) => gameData != null;

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