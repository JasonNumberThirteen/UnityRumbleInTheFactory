public class StageSelectionSceneManager : GameSceneManager
{
	public string stageSceneName;
	public GameData gameData;
	public PlayerData playerOneData, playerTwoData;
	public StagesLoader stagesLoader;

	public void StartGame(int stage)
	{
		gameData.stageNumber = stage;
		gameData.stage = stagesLoader.StageByIndex(stage - 1);

		gameData.ResetData();
		playerOneData.ResetData();
		playerTwoData.ResetData();
		LoadScene(stageSceneName);
	}
}