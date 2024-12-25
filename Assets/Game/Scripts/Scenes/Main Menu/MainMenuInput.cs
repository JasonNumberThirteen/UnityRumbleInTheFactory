public class MainMenuInput : MenuOptionsInput
{
	public MainMenuOptionsController optionsController;
	public Timer mainMenuPanelTimer;

	private void Awake()
	{
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
			navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
			submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
		}
		else
		{
			navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
			submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(mainMenuPanelTimer.Finished)
		{
			if(direction == -1)
			{
				SelectPreviousOption();
			}
			else if(direction == 1)
			{
				SelectNextOption();
			}
		}
		else
		{
			mainMenuPanelTimer.InterruptTimer();
		}
	}

	private void OnSubmitKeyPressed()
	{
		if(mainMenuPanelTimer.Finished)
		{
			optionsController.SubmitOption();
		}
		else
		{
			mainMenuPanelTimer.InterruptTimer();
		}
	}

	private void SelectPreviousOption()
	{
		optionsController.DecreaseOptionValue();
		optionsController.SelectOption();
	}

	private void SelectNextOption()
	{
		optionsController.IncreaseOptionValue();
		optionsController.SelectOption();
	}
}