using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
	public readonly string MAIN_MENU_SCENE_NAME = "MainMenuScene";
	public readonly string STAGE_SELECTION_SCENE_NAME = "StageSelectionScene";
	public readonly string STAGE_SCENE_NAME = "StageScene";
	public readonly string SCORE_SCENE_NAME = "ScoreScene";
	public readonly string GAME_OVER_SCENE_NAME = "GameOverScene";
	public readonly string HIGH_SCORE_SCENE_NAME = "HighScoreScene";
	
	public void LoadSceneByName(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}