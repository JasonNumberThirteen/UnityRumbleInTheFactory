using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private MenuOptionsInputController menuOptionsInputController;
	private DataSerialisationManager dataSerialisationManager;

	private void Awake()
	{
		menuOptionsInputController = ObjectMethods.FindComponentOfType<MenuOptionsInputController>();
		dataSerialisationManager = ObjectMethods.FindComponentOfType<DataSerialisationManager>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
				menuOptionsInputController.cancelKeyPressedEvent.AddListener(OnCancelKeyPressed);
			}
		}
		else
		{
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
				menuOptionsInputController.cancelKeyPressedEvent.RemoveListener(OnCancelKeyPressed);
			}
		}
	}

	private void OnSubmitKeyPressed()
	{
		if(!GameDataMethods.AnyStageFound(gameData))
		{
			return;
		}
		
		ResetGameData();
		SetStageNumber();
		ResetPlayerRobotsListData();
		LoadSceneByName(STAGE_SCENE_NAME);
	}

	private void OnCancelKeyPressed()
	{
		SetStageNumber();
		LoadSceneByName(MAIN_MENU_SCENE_NAME);
	}

	private void ResetGameData()
	{
		if(GameDataMethods.GameDataIsDefined(gameData))
		{
			gameData.ResetData();
		}
	}

	private void SetStageNumber()
	{
		var stageCounterHeaderStageSelectionTextUI = ObjectMethods.FindComponentOfType<StageCounterHeaderStageSelectionTextUI>();
		
		if(gameData == null || stageCounterHeaderStageSelectionTextUI == null)
		{
			return;
		}
		
		gameData.SetStageNumber(stageCounterHeaderStageSelectionTextUI.GetCurrentCounterValue());
		SaveGameData();
	}

	private void SaveGameData()
	{
		if(dataSerialisationManager != null)
		{
			dataSerialisationManager.SerialiseGameData();
		}
	}

	private void ResetPlayerRobotsListData()
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.GetPlayerRobotsData().ForEachIndexed(ResetPlayerRobotData, 1);
		}
	}

	private void ResetPlayerRobotData(PlayerRobotData playerRobotData, int counterValue)
	{
		var isFirstPlayer = counterValue == 1;
		
		playerRobotData.ResetData(isFirstPlayer || GameDataMethods.SelectedTwoPlayersMode(gameData));
	}
}