public class StageSelectionSceneManager : GameSceneManager
{
	public string mainMenuSceneName, stageSceneName;
	public GameData gameData;
	public PlayerData playerOneData, playerTwoData;

	public void StartGame(int stage)
	{
		gameData.stageNumber = stage;

		gameData.ResetData();
		playerOneData.ResetData();
		playerTwoData.ResetData();
		LoadScene(stageSceneName);
	}

	public void BackToMainMenu() => LoadScene(mainMenuSceneName);
}