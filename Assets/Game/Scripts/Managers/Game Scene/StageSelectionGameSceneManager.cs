using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private MenuOptionsInput menuOptionsInput;
	private StageCounterStageSelectionTextUI stageCounterTextUI;

	private void Awake()
	{
		menuOptionsInput = FindAnyObjectByType<MenuOptionsInput>();
		stageCounterTextUI = FindAnyObjectByType<StageCounterStageSelectionTextUI>();

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
		SetInitialStageNumber();
		ResetPlayerRobotsListData();
		LoadSceneByName(STAGE_SCENE_NAME);
	}

	private void OnCancelKeyPressed()
	{
		LoadSceneByName(MAIN_MENU_SCENE_NAME);
	}

	private void ResetGameData()
	{
		if(gameData != null)
		{
			gameData.ResetData();
		}
	}

	private void SetInitialStageNumber()
	{
		if(gameData != null && stageCounterTextUI != null)
		{
			gameData.SetStageNumber(stageCounterTextUI.GetCurrentCounterValue());
		}
	}

	private void ResetPlayerRobotsListData()
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEach(playerRobotData => playerRobotData.ResetData());
		}
	}
}