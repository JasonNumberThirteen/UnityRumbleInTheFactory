public class StageSelectionGameSceneManager : GameSceneManager
{
	public string mainMenuSceneName, stageSceneName;
	public GameData gameData;
	public PlayerData[] playersData;

	public void BackToMainMenu() => LoadScene(mainMenuSceneName);

	public void StartGame(int stage)
	{
		gameData.ResetData(stage);
		ResetPlayersData();
		LoadScene(stageSceneName);
	}

	private void ResetPlayersData()
	{
		foreach (PlayerData pd in playersData)
		{
			pd.ResetData();
		}
	}
}