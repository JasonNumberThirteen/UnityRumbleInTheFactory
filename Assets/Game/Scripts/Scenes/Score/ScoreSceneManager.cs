public class ScoreSceneManager : GameSceneManager
{
	public GameData gameData;
	
	public string stageSceneName, gameOverSceneName;

	public void GoToNextScene()
	{
		bool isOver = gameData.GameIsOver;
		string sceneName = isOver ? gameOverSceneName : stageSceneName;
		
		if(!isOver)
		{
			gameData.IncreaseDifficultyIfNeeded();
			gameData.AdvanceToNextStage();
		}

		LoadSceneByName(sceneName);
	}
}