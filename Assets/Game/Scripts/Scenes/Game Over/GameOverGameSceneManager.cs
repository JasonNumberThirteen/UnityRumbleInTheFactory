using UnityEngine;

public class GameOverGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;

	public void GoToNextScene()
	{
		var beatenHighScore = gameData.beatenHighScore;
		var sceneName = beatenHighScore ? HIGH_SCORE_SCENE_NAME : MAIN_MENU_SCENE_NAME;

		LoadSceneByName(sceneName);
	}
}