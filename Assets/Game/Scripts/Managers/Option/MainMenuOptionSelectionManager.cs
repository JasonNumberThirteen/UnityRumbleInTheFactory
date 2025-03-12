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
			loopingIntCounter.valueChangedEvent.AddListener(OnCounterValueChanged);
			
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
				menuOptionsInputController.submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
			}
		}
		else
		{
			loopingIntCounter.valueChangedEvent.RemoveListener(OnCounterValueChanged);
			
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
				menuOptionsInputController.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
			}
		}
	}

	private void OnCounterValueChanged()
	{
		if(optionsManager != null)
		{
			optionsManager.SelectOptionIfPossible(GetOptionTypeByCounterValue());
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		TriggerOnKeyPressed(() => loopingIntCounter.ModifyBy(direction));
	}

	private void OnSubmitKeyPressed()
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