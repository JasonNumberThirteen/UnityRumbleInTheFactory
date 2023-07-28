public class ScoreSceneManager : GameSceneManager
{
	public GameData gameData;
	
	public string stageSceneName, gameOverSceneName;

	public void GoToNextScene()
	{
		bool isOver = gameData.isOver;
		string sceneName = isOver ? gameOverSceneName : stageSceneName;
		
		if(!isOver)
		{
			++gameData.stageNumber;
		}

		LoadScene(sceneName);
	}
}