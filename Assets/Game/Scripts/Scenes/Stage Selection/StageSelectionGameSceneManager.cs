using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private string mainMenuSceneName;
	[SerializeField] private string stageSceneName;
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerData[] playersData;

	public void BackToMainMenu() => LoadSceneByName(mainMenuSceneName);

	public void StartGame(int stage)
	{
		gameData.ResetData(stage);
		ResetPlayersData();
		LoadSceneByName(stageSceneName);
	}

	private void ResetPlayersData()
	{
		foreach (PlayerData pd in playersData)
		{
			pd.ResetData();
		}
	}
}