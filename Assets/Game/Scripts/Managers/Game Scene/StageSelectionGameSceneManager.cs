using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private MenuOptionsInput menuOptionsInput;
	private DataSerialisationManager dataSerialisationManager;

	private void Awake()
	{
		menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();
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
			if(menuOptionsInput != null)
			{
				menuOptionsInput.submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
				menuOptionsInput.cancelKeyPressedEvent.AddListener(OnCancelKeyPressed);
			}
		}
		else
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
				menuOptionsInput.cancelKeyPressedEvent.RemoveListener(OnCancelKeyPressed);
			}
		}
	}

	private void OnSubmitKeyPressed()
	{
		if(GameDataMethods.NoStagesFound(gameData))
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
		if(gameData != null)
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
		var selectedTwoPlayersMode = gameData != null && gameData.SelectedTwoPlayersMode;
		
		playerRobotData.ResetData(isFirstPlayer || selectedTwoPlayersMode);
	}
}