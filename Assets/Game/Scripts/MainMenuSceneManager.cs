using UnityEngine;

public class MainMenuSceneManager : MonoBehaviour
{
	public void OnePlayer() => StartGame(false);
	public void TwoPlayers() => StartGame(true);
	public void Exit() => Application.Quit();

	private void StartGame(bool twoPlayersMode)
	{
		Debug.Log(twoPlayersMode ? "Two players mode" : "One player mode");
	}
}