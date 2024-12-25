using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	[SerializeField] private MainMenuCounter[] counters;
	[SerializeField] private MainMenuPanelUI mainMenuPanelUI;
	[SerializeField] private GameObject optionsCursorGO;

	private void Awake()
	{
		RegisterToListeners(true);
	}

	private void Start()
	{
		SetCounterValues();
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
	
	private void SetCounterValues()
	{
		foreach (MainMenuCounter mmc in counters)
		{
			mmc.SetCounterValue();
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

[System.Serializable]
public class MainMenuCounter
{
	[SerializeField] private MainMenuData data;
	[SerializeField] private IntCounter counter;

	public void SetCounterValue() => counter.SetTo(data.MainMenuCounterValue());
}