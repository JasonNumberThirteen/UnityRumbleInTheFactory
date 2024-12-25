using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	private MainMenuPanelUI mainMenuPanelUI;
	private MainMenuOptionsCursor mainMenuOptionsCursor;
	private MainMenuOptionsListenersManager mainMenuOptionsListenersManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		mainMenuPanelUI = FindAnyObjectByType<MainMenuPanelUI>();
		mainMenuOptionsCursor = FindAnyObjectByType<MainMenuOptionsCursor>(FindObjectsInactive.Include);
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
				mainMenuPanelUI.panelReachedTargetPositionEvent.AddListener(ActivateOptionsCursor);
			}

			if(mainMenuOptionsListenersManager != null)
			{
				mainMenuOptionsListenersManager.gameStartOptionSelectedEvent.AddListener(OnGameStartOptionSelected);
			}
		}
		else
		{
			if(mainMenuPanelUI != null)
			{
				mainMenuPanelUI.panelReachedTargetPositionEvent.RemoveListener(ActivateOptionsCursor);
			}

			if(mainMenuOptionsListenersManager != null)
			{
				mainMenuOptionsListenersManager.gameStartOptionSelectedEvent.RemoveListener(OnGameStartOptionSelected);
			}
		}
	}

	private void ActivateOptionsCursor()
	{
		if(mainMenuOptionsCursor != null)
		{
			mainMenuOptionsCursor.SetActive(true);
		}
	}

	private void OnGameStartOptionSelected()
	{
		if(translationBackgroundPanelUI != null)
		{
			translationBackgroundPanelUI.StartTranslation();
		}
	}
}