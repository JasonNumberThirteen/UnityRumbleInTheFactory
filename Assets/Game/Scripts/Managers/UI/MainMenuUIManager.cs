using UnityEngine;

public class MainMenuUIManager : UIManager
{
	private MainMenuPanelUI mainMenuPanelUI;
	private MainMenuOptionsCursorImageUI mainMenuOptionsCursorImageUI;
	private MainMenuOptionsListenersManager mainMenuOptionsListenersManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		mainMenuPanelUI = FindAnyObjectByType<MainMenuPanelUI>();
		mainMenuOptionsCursorImageUI = FindAnyObjectByType<MainMenuOptionsCursorImageUI>(FindObjectsInactive.Include);
		mainMenuOptionsListenersManager = FindAnyObjectByType<MainMenuOptionsListenersManager>(FindObjectsInactive.Include);
		translationBackgroundPanelUI = FindAnyObjectByType<TranslationBackgroundPanelUI>(FindObjectsInactive.Include);
		
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