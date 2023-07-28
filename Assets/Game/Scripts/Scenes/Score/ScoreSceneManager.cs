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
			if(++gameData.stageNumber > gameData.stages.Length)
			{
				gameData.stageNumber = 1;
			}
		}

		LoadScene(sceneName);
	}
}