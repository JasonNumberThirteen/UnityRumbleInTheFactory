using System;
using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class MainMenuOptionSelectionManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private LoopingIntCounter loopingIntCounter;
	private MenuOptionsInput menuOptionsInput;
	private MainMenuPanelUI mainMenuPanelUI;
	private OptionsManager optionsManager;

	private void Awake()
	{
		loopingIntCounter = GetComponent<LoopingIntCounter>();
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		mainMenuPanelUI = FindFirstObjectByType<MainMenuPanelUI>();
		optionsManager = FindFirstObjectByType<OptionsManager>();
		
		RegisterToListeners(true);
	}

	private void Start()
	{
		SetCounterRange();
		SelectTwoPlayersModeIfNeeded();
	}

	private void SetCounterRange()
	{
		var numberOfOptions = optionsManager != null ? optionsManager.GetNumberOfOptions() : 0;
		var max = Mathf.Max(1, numberOfOptions);
		
		loopingIntCounter.SetRange(1, max);
	}

	private void SelectTwoPlayersModeIfNeeded()
	{
		if(gameData != null && gameData.EnteredStageSelection && gameData.SelectedTwoPlayersMode)
		{
			loopingIntCounter.SetTo((int)OptionType.TwoPlayersMode + 1);
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
			}

			loopingIntCounter.valueChangedEvent.AddListener(OnCounterValueChanged);
		}
		else
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
			}

			loopingIntCounter.valueChangedEvent.RemoveListener(OnCounterValueChanged);
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		TriggerOnKeyPressed(() =>
		{
			if(direction == -1)
			{
				loopingIntCounter.DecreaseBy(1);
			}
			else if(direction == 1)
			{
				loopingIntCounter.IncreaseBy(1);
			}
		});
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

	private void OnCounterValueChanged()
	{
		if(optionsManager != null)
		{
			optionsManager.SelectOptionIfPossible(GetOptionTypeByCounterValue());
		}
	}

	private OptionType GetOptionTypeByCounterValue()
	{
		var index = loopingIntCounter.CurrentValue - 1;

		return (OptionType)index;
	}
}