using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
	public static readonly string MAIN_MENU_SCENE_NAME = "MainMenuScene";
	public static readonly string STAGE_SELECTION_SCENE_NAME = "StageSelectionScene";
	public static readonly string STAGE_SCENE_NAME = "StageScene";
	public static readonly string SCORE_SCENE_NAME = "ScoreScene";
	public static readonly string GAME_OVER_SCENE_NAME = "GameOverScene";
	public static readonly string HIGH_SCORE_SCENE_NAME = "HighScoreScene";
	
	public void LoadSceneByName(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}