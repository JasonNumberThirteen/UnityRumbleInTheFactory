using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	[SerializeField] private MainMenuPanelUI mainMenuPanelUI;
	[SerializeField] private GameObject optionsCursorGO;

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