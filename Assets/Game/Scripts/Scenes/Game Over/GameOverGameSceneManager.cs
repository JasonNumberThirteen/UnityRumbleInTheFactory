public class GameOverGameSceneManager : GameSceneManager
{
	public GameData gameData;
	
	public string mainMenuSceneName, highScoreSceneName;

	public void GoToNextScene()
	{
		var beatenHighScore = gameData.beatenHighScore;
		var sceneName = beatenHighScore ? highScoreSceneName : mainMenuSceneName;

		LoadScene(sceneName);
	}
}