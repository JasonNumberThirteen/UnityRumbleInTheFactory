using UnityEngine;

public class StageSelectionGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayersListData playersListData;

	private MenuOptionsInput menuOptionsInput;
	private StageCounterStageSelectionTextUI stageCounterTextUI;

	private void Awake()
	{
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		stageCounterTextUI = FindFirstObjectByType<StageCounterStageSelectionTextUI>();

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
		if(stageCounterTextUI == null || gameData == null || gameData.NoStagesFound())
		{
			return;
		}
		
		ResetGameData();
		ResetPlayersListData();
		LoadSceneByName(STAGE_SCENE_NAME);
	}

	private void OnCancelKeyPressed()
	{
		LoadSceneByName(MAIN_MENU_SCENE_NAME);
	}

	private void ResetGameData()
	{
		if(gameData != null && stageCounterTextUI != null)
		{
			gameData.ResetData(stageCounterTextUI.GetCurrentCounterValue());
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