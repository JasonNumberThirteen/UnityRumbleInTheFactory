using UnityEngine;
using UnityEngine.Events;

public class MainMenuOptionsListenersManager : MonoBehaviour
{
	public UnityEvent gameStartOptionSubmittedEvent;
	
	[SerializeField] private GameData gameData;
	
	private OptionsManager optionsManager;
	private MenuOptionsInputController menuOptionsInputController;
	private MainMenuOptionsCursorImageUI mainMenuOptionsCursorImageUI;

	private void Awake()
	{
		optionsManager = ObjectMethods.FindComponentOfType<OptionsManager>();
		menuOptionsInputController = ObjectMethods.FindComponentOfType<MenuOptionsInputController>();
		mainMenuOptionsCursorImageUI = ObjectMethods.FindComponentOfType<MainMenuOptionsCursorImageUI>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(optionsManager == null)
		{
			return;
		}
		
		optionsManager.RegisterToOptionListeners(register, OptionType.OnePlayerMode, OnOptionSelected, OnOnePlayerModeSubmitted);
		optionsManager.RegisterToOptionListeners(register, OptionType.TwoPlayerMode, OnOptionSelected, OnTwoPlayerModeSubmitted);
		optionsManager.RegisterToOptionListeners(register, OptionType.ExitGame, OnOptionSelected, OnExitGameSubmitted);
	}

	private void OnOptionSelected(Option option)
	{
		if(mainMenuOptionsCursorImageUI != null)
		{
			mainMenuOptionsCursorImageUI.SetPositionRelativeToOption(option);
		}
	}

	private void OnOnePlayerModeSubmitted(Option option)
	{
		StartGame(false);
	}

	private void OnTwoPlayerModeSubmitted(Option option)
	{
		StartGame(true);
	}

	private void StartGame(bool twoPlayerMode)
	{
		SetupGameDataForStart(twoPlayerMode);
		DeactivateMenuOptionsInputController();
		gameStartOptionSubmittedEvent?.Invoke();
	}

	private void SetupGameDataForStart(bool twoPlayerMode)
	{
		if(GameDataMethods.GameDataIsDefined(gameData))
		{
			gameData.SetupForGameStart(twoPlayerMode);
		}
	}

	private void DeactivateMenuOptionsInputController()
	{
		if(menuOptionsInputController != null)
		{
			menuOptionsInputController.SetActive(false);
		}
	}

	private void OnExitGameSubmitted(Option option)
	{
		Application.Quit();
	}
}