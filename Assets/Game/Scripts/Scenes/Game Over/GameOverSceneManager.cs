public class GameOverSceneManager : GameSceneManager
{
	public GameData gameData;
	
	public string mainMenuSceneName, highScoreSceneName;

	public void GoToNextScene()
	{
		bool beatenHighScore = gameData.beatenHighScore;
		string sceneName = beatenHighScore ? highScoreSceneName : mainMenuSceneName;

		LoadScene(sceneName);
	}
}