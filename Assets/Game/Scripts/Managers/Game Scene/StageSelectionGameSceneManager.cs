using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private MenuOptionsInput menuOptionsInput;
	private StageCounterHeaderStageSelectionTextUI stageCounterHeaderTextUI;
	private DataSerialisationManager dataSerialisationManager;

	private void Awake()
	{
		menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();
		stageCounterHeaderTextUI = ObjectMethods.FindComponentOfType<StageCounterHeaderStageSelectionTextUI>();
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
		if(gameData == null || GetStageCounterHeaderStageSelectionTextUI() == null)
		{
			return;
		}
		
		gameData.SetStageNumber(stageCounterHeaderTextUI.GetCurrentCounterValue());
		SaveGameData();
	}

	private StageCounterHeaderStageSelectionTextUI GetStageCounterHeaderStageSelectionTextUI()
	{
		if(stageCounterHeaderTextUI == null)
		{
			stageCounterHeaderTextUI = ObjectMethods.FindComponentOfType<StageCounterHeaderStageSelectionTextUI>();
		}

		return stageCounterHeaderTextUI;
	}

	private void SaveGameData()
	{
		if(dataSerialisationManager != null)
		{
			dataSerialisationManager.SaveGameData();
		}
	}

	private void ResetPlayerRobotsListData()
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEachIndexed((playerRobotData, i) => playerRobotData.ResetData(i == 1 || (gameData != null && gameData.SelectedTwoPlayersMode)), 1);
		}
	}
}