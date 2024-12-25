using UnityEngine;
using UnityEngine.Events;

public class MainMenuOptionsListenersManager : MonoBehaviour
{
	public UnityEvent gameStartOptionSubmittedEvent;
	
	[SerializeField] private GameData gameData;
	
	private OptionsManager optionsManager;
	private MenuOptionsInput menuOptionsInput;
	private MainMenuOptionsCursor mainMenuOptionsCursor;

	private void Awake()
	{
		optionsManager = FindFirstObjectByType<OptionsManager>();
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>(FindObjectsInactive.Include);
		mainMenuOptionsCursor = FindFirstObjectByType<MainMenuOptionsCursor>(FindObjectsInactive.Include);

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
		
		optionsManager.RegisterToOptionListeners(register, OptionType.OnePlayerMode, OnOnePlayerModeSelected, OnOnePlayerModeSubmitted);
		optionsManager.RegisterToOptionListeners(register, OptionType.TwoPlayersMode, OnTwoPlayersModeSelected, OnTwoPlayersModeSubmitted);
		optionsManager.RegisterToOptionListeners(register, OptionType.ExitGame, OnExitGameSelected, OnExitGameSubmitted);
	}

	private void OnOnePlayerModeSelected()
	{
		SetPositionYToOptionsCursor(-44);
	}

	private void OnTwoPlayersModeSelected()
	{
		SetPositionYToOptionsCursor(-60);
	}

	private void OnExitGameSelected()
	{
		SetPositionYToOptionsCursor(-76);
	}

	private void SetPositionYToOptionsCursor(float y)
	{
		if(mainMenuOptionsCursor != null)
		{
			mainMenuOptionsCursor.SetPositionY(y);
		}
	}

	private void OnOnePlayerModeSubmitted()
	{
		StartGame(false);
	}

	private void OnTwoPlayersModeSubmitted()
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
		if(gameData == null)
		{
			return;
		}

		gameData.enteredStageSelection = true;
		gameData.twoPlayersMode = twoPlayersMode;
	}

	private void DeactivateMenuOptionsInput()
	{
		if(menuOptionsInput != null)
		{
			menuOptionsInput.SetActive(false);
		}
	}

	private void OnExitGameSubmitted()
	{
		Application.Quit();
	}
}