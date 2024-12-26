using UnityEngine;

[RequireComponent(typeof(Timer))]
public class GameOverGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;

	private Timer timer;

	private void Awake()
	{
		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}
	
	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerEnd()
	{
		var beatenHighScore = gameData != null && gameData.beatenHighScore;
		var sceneName = beatenHighScore ? HIGH_SCORE_SCENE_NAME : MAIN_MENU_SCENE_NAME;

		LoadSceneByName(sceneName);
	}
}