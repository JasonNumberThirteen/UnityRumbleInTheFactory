using UnityEngine;

public class GameOverGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private string mainMenuSceneName;
	[SerializeField] private string highScoreSceneName;

	public void GoToNextScene()
	{
		var beatenHighScore = gameData.beatenHighScore;
		var sceneName = beatenHighScore ? highScoreSceneName : mainMenuSceneName;

		LoadSceneByName(sceneName);
	}
}