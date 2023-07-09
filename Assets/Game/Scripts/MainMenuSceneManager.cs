using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
	public Timer stageSelectionBackgroundTimer;
	public MainMenuInput input;
	
	public void OnePlayer() => StartGame(false);
	public void TwoPlayers() => StartGame(true);
	public void Exit() => Application.Quit();

	private void StartGame(bool twoPlayersMode)
	{
		stageSelectionBackgroundTimer.StartTimer();
		input.gameObject.SetActive(false);
	}
}