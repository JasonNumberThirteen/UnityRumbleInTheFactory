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

	private void Start()
	{
		CursorMethods.SetCursorVisible(false);
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
				mainMenuPanelUI.targetPositionWasReachedEvent.AddListener(ActivateOptionsCursorImageUI);
			}

			if(mainMenuOptionsListenersManager != null)
			{
				mainMenuOptionsListenersManager.gameStartOptionWasSubmittedEvent.AddListener(OnGameStartOptionSubmitted);
			}
		}
		else
		{
			if(mainMenuPanelUI != null)
			{
				mainMenuPanelUI.targetPositionWasReachedEvent.RemoveListener(ActivateOptionsCursorImageUI);
			}

			if(mainMenuOptionsListenersManager != null)
			{
				mainMenuOptionsListenersManager.gameStartOptionWasSubmittedEvent.RemoveListener(OnGameStartOptionSubmitted);
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

	private void OnApplicationQuit()
	{
		CursorMethods.SetCursorVisible(true);
	}
}