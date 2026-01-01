using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class MainMenuOptionSelectionManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private LoopingIntCounter loopingIntCounter;
	private MenuOptionsInputController menuOptionsInputController;
	private MainMenuPanelUI mainMenuPanelUI;
	private OptionsManager optionsManager;

	private void Awake()
	{
		loopingIntCounter = GetComponent<LoopingIntCounter>();
		menuOptionsInputController = ObjectMethods.FindComponentOfType<MenuOptionsInputController>();
		mainMenuPanelUI = ObjectMethods.FindComponentOfType<MainMenuPanelUI>();
		optionsManager = ObjectMethods.FindComponentOfType<OptionsManager>();
		
		RegisterToListeners(true);
	}

	private void Start()
	{
		SetCounterRange();
		SelectTwoPlayerModeIfNeeded();
	}

	private void SetCounterRange()
	{
		var numberOfOptions = optionsManager != null ? optionsManager.GetNumberOfOptions() : 0;
		var max = Mathf.Max(1, numberOfOptions);
		
		loopingIntCounter.SetRange(1, max);
	}

	private void SelectTwoPlayerModeIfNeeded()
	{
		if(GameDataMethods.EnteredStageSelection(gameData) && GameDataMethods.SelectedTwoPlayerMode(gameData))
		{
			StartCoroutine(SelectTwoPlayerMode());
		}
	}

	private IEnumerator SelectTwoPlayerMode()
	{
		yield return new WaitForEndOfFrame();

		loopingIntCounter.SetTo(OptionType.TwoPlayerMode.ToInt());
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			loopingIntCounter.valueWasChangedEvent.AddListener(OnValueWasChanged);
			
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.navigateKeyWasPressedEvent.AddListener(OnNavigateKeyWasPressed);
				menuOptionsInputController.submitKeyWasPressedEvent.AddListener(OnSubmitKeyWasPressed);
			}
		}
		else
		{
			loopingIntCounter.valueWasChangedEvent.RemoveListener(OnValueWasChanged);
			
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.navigateKeyWasPressedEvent.RemoveListener(OnNavigateKeyWasPressed);
				menuOptionsInputController.submitKeyWasPressedEvent.RemoveListener(OnSubmitKeyWasPressed);
			}
		}
	}

	private void OnValueWasChanged()
	{
		if(optionsManager != null)
		{
			optionsManager.SelectOptionIfPossible(GetOptionTypeByCounterValue());
		}
	}

	private void OnNavigateKeyWasPressed(int direction)
	{
		TriggerOnKeyPressed(() => loopingIntCounter.ModifyBy(direction));
	}

	private void OnSubmitKeyWasPressed()
	{
		if(optionsManager != null)
		{
			TriggerOnKeyPressed(() => optionsManager.SubmitOptionIfPossible(GetOptionTypeByCounterValue()));
		}
	}

	private void TriggerOnKeyPressed(Action onKeyPressed)
	{
		if(mainMenuPanelUI == null)
		{
			return;
		}

		if(mainMenuPanelUI.ReachedTargetPosition)
		{
			onKeyPressed?.Invoke();
		}
		else
		{
			mainMenuPanelUI.SetTargetPosition();
		}
	}

	private OptionType GetOptionTypeByCounterValue() => loopingIntCounter.CurrentValue.ToEnumValue<OptionType>();
}