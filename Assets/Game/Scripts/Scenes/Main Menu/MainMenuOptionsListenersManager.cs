using UnityEngine;
using UnityEngine.Events;

public class MainMenuOptionsListenersManager : MonoBehaviour
{
	public UnityEvent gameStartOptionSelectedEvent;
	
	public GameData gameData;
	public Timer stageSelectionBackgroundTimer;
	public MenuOptionsInput input;

	private OptionsManager optionsManager;
	private MainMenuOptionsCursor mainMenuOptionsCursor;

	private void Awake()
	{
		optionsManager = FindFirstObjectByType<OptionsManager>();
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
		mainMenuOptionsCursor.SetPositionY(-44);
	}

	private void OnTwoPlayersModeSelected()
	{
		mainMenuOptionsCursor.SetPositionY(-60);
	}

	private void OnExitGameSelected()
	{
		mainMenuOptionsCursor.SetPositionY(-76);
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
		gameData.enteredStageSelection = true;
		gameData.twoPlayersMode = twoPlayersMode;
		
		stageSelectionBackgroundTimer.StartTimer();
		input.gameObject.SetActive(false);
		gameStartOptionSelectedEvent?.Invoke();
	}

	private void OnExitGameSubmitted()
	{
		Application.Quit();
	}
}