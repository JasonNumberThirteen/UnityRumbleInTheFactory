public class MainMenuUIManager : UIManager
{
	private MainMenuPanelUI mainMenuPanelUI;
	private MainMenuOptionsListenersManager mainMenuOptionsListenersManager;
	private MainMenuOptionsCursorImageUI mainMenuOptionsCursorImageUI;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		mainMenuPanelUI = ObjectMethods.FindComponentOfType<MainMenuPanelUI>();
		mainMenuOptionsListenersManager = ObjectMethods.FindComponentOfType<MainMenuOptionsListenersManager>();
		mainMenuOptionsCursorImageUI = ObjectMethods.FindComponentOfType<MainMenuOptionsCursorImageUI>();
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();
		
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
			if(mainMenuPanelUI != null)
			{
				mainMenuPanelUI.panelReachedTargetPositionEvent.AddListener(ActivateOptionsCursorImageUI);
			}

			if(mainMenuOptionsListenersManager != null)
			{
				mainMenuOptionsListenersManager.gameStartOptionSubmittedEvent.AddListener(OnGameStartOptionSubmitted);
			}
		}
		else
		{
			if(mainMenuPanelUI != null)
			{
				mainMenuPanelUI.panelReachedTargetPositionEvent.RemoveListener(ActivateOptionsCursorImageUI);
			}

			if(mainMenuOptionsListenersManager != null)
			{
				mainMenuOptionsListenersManager.gameStartOptionSubmittedEvent.RemoveListener(OnGameStartOptionSubmitted);
			}
		}
	}

	private void ActivateOptionsCursorImageUI()
	{
		if(mainMenuOptionsCursorImageUI != null)
		{
			mainMenuOptionsCursorImageUI.SetActive(true);
		}
	}

	private void OnGameStartOptionSubmitted()
	{
		if(translationBackgroundPanelUI != null)
		{
			translationBackgroundPanelUI.StartTranslation();
		}
	}
}