using UnityEngine;
using UnityEngine.Events;

public class MainMenuOptionsListenersManager : MonoBehaviour
{
	public UnityEvent gameStartOptionSubmittedEvent;
	
	[SerializeField] private GameData gameData;
	
	private OptionsManager optionsManager;
	private MenuOptionsInput menuOptionsInput;
	private MainMenuOptionsCursorImageUI mainMenuOptionsCursorImageUI;

	private void Awake()
	{
		optionsManager = ObjectMethods.FindComponentOfType<OptionsManager>();
		menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();
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
		optionsManager.RegisterToOptionListeners(register, OptionType.TwoPlayersMode, OnOptionSelected, OnTwoPlayersModeSubmitted);
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

	private void OnTwoPlayersModeSubmitted(Option option)
	{
		StartGame(true);
	}

	private void StartGame(bool twoPlayersMode)
	{
		SetupGameData(twoPlayersMode);
		DeactivateMenuOptionsInput();
		gameStartOptionSubmittedEvent?.Invoke();
	}

	private void SetupGameData(bool twoPlayersMode)
	{
		if(gameData != null)
		{
			gameData.SetupForGameStart(twoPlayersMode);
		}
	}

	private void DeactivateMenuOptionsInput()
	{
		if(menuOptionsInput != null)
		{
			menuOptionsInput.SetActive(false);
		}
	}

	private void OnExitGameSubmitted(Option option)
	{
		Application.Quit();
	}
}