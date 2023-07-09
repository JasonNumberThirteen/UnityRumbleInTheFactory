using UnityEngine;

public class MainMenuSceneManager : MonoBehaviour
{
	public Timer stageSelectionBackgroundTimer;
	
	public void OnePlayer() => StartGame(false);
	public void TwoPlayers() => StartGame(true);
	public void Exit() => Application.Quit();

	private void StartGame(bool twoPlayersMode)
	{
		stageSelectionBackgroundTimer.StartTimer();
	}
}