using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	private MainMenuPanelUI mainMenuPanelUI;
	private MainMenuOptionsCursor mainMenuOptionsCursor;

	private void Awake()
	{
		mainMenuPanelUI = FindAnyObjectByType<MainMenuPanelUI>();
		mainMenuOptionsCursor = FindAnyObjectByType<MainMenuOptionsCursor>(FindObjectsInactive.Include);
		
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
		}
		else
		{
			if(mainMenuPanelUI != null)
			{
				mainMenuPanelUI.panelReachedTargetPositionEvent.RemoveListener(ActivateOptionsCursor);
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
}