using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayersListData playersListData;

	public void BackToMainMenu() => LoadSceneByName(MAIN_MENU_SCENE_NAME);

	public void StartGame(int initialStageNumber)
	{
		ResetGameData(initialStageNumber);
		ResetPlayersListData();
		LoadSceneByName(STAGE_SCENE_NAME);
	}

	private void ResetGameData(int initialStageNumber)
	{
		if(gameData != null)
		{
			gameData.ResetData(initialStageNumber);
		}
	}

	private void ResetPlayersListData()
	{
		if(playersListData != null)
		{
			playersListData.ResetPlayersData();
		}
	}
}