public class StageSelectionSceneManager : GameSceneManager
{
	public string mainMenuSceneName, stageSceneName;
	public GameData gameData;
	public PlayerData playerOneData, playerTwoData;

	public void BackToMainMenu() => LoadScene(mainMenuSceneName);

	public void StartGame(int stage)
	{
		gameData.StageNumber = stage;

		gameData.ResetData();
		playerOneData.ResetData();
		playerTwoData.ResetData();
		LoadScene(stageSceneName);
	}
}