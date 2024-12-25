using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	[SerializeField] private GameObject optionsCursorGO;

	private MainMenuPanelUI mainMenuPanelUI;

	private void Awake()
	{
		mainMenuPanelUI = FindAnyObjectByType<MainMenuPanelUI>();
		
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
				mainMenuPanelUI.panelReachedTargetPositionEvent.AddListener(ActivateOptionsCursorGO);
			}
		}
		else
		{
			if(mainMenuPanelUI != null)
			{
				mainMenuPanelUI.panelReachedTargetPositionEvent.RemoveListener(ActivateOptionsCursorGO);
			}
		}
	}

	private void ActivateOptionsCursorGO()
	{
		if(optionsCursorGO != null)
		{
			optionsCursorGO.SetActive(true);
		}
	}
}