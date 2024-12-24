using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerData[] playersData;

	public void BackToMainMenu() => LoadSceneByName(MAIN_MENU_SCENE_NAME);

	public void StartGame(int stage)
	{
		gameData.ResetData(stage);
		ResetPlayersData();
		LoadSceneByName(STAGE_SCENE_NAME);
	}

	private void ResetPlayersData()
	{
		foreach (PlayerData pd in playersData)
		{
			pd.ResetData();
		}
	}
}