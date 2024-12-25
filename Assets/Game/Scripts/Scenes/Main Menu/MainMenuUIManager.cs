using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	private MainMenuPanelUI mainMenuPanelUI;
	private MainMenuOptionsCursor mainMenuOptionsCursor;
	private MainMenuOptionsListenersManager mainMenuOptionsListenersManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;
	private GameSceneManager gameSceneManager;

	private void Awake()
	{
		mainMenuPanelUI = FindAnyObjectByType<MainMenuPanelUI>();
		mainMenuOptionsCursor = FindAnyObjectByType<MainMenuOptionsCursor>(FindObjectsInactive.Include);
		mainMenuOptionsListenersManager = FindAnyObjectByType<MainMenuOptionsListenersManager>(FindObjectsInactive.Include);
		translationBackgroundPanelUI = FindAnyObjectByType<TranslationBackgroundPanelUI>(FindObjectsInactive.Include);
		gameSceneManager = FindAnyObjectByType<GameSceneManager>(FindObjectsInactive.Include);
		
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
				mainMenuOptionsListenersManager.gameStartOptionSubmittedEvent.AddListener(OnGameStartOptionSubmitted);
			}

			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.panelFinishedTranslationEvent.AddListener(OnPanelFinishedTranslation);
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
				mainMenuOptionsListenersManager.gameStartOptionSubmittedEvent.RemoveListener(OnGameStartOptionSubmitted);
			}

			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.panelFinishedTranslationEvent.RemoveListener(OnPanelFinishedTranslation);
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

	private void OnGameStartOptionSubmitted()
	{
		if(translationBackgroundPanelUI != null)
		{
			translationBackgroundPanelUI.StartTranslation();
		}
	}

	private void OnPanelFinishedTranslation()
	{
		if(gameSceneManager != null)
		{
			gameSceneManager.LoadSceneByName(gameSceneManager.STAGE_SELECTION_SCENE_NAME);
		}
	}
}