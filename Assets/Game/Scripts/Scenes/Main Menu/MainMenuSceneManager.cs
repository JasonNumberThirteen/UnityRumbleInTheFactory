using UnityEngine;

public class MainMenuSceneManager : GameSceneManager
{
	public GameData gameData;
	public Timer stageSelectionBackgroundTimer;
	public MenuOptionsInput input;
	
	public void OnePlayer() => StartGame(false);
	public void TwoPlayers() => StartGame(true);
	public void Exit() => Application.Quit();

	private void StartGame(bool twoPlayersMode)
	{
		gameData.enteredStageSelection = true;
		gameData.twoPlayersMode = twoPlayersMode;
		
		stageSelectionBackgroundTimer.StartTimer();
		input.gameObject.SetActive(false);
	}
}