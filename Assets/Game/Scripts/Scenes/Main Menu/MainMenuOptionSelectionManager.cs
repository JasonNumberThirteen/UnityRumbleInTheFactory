using System;
using UnityEngine;

[RequireComponent(typeof(LoopingCounter))]
public class MainMenuOptionSelectionManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private LoopingCounter loopingCounter;
	private MenuOptionsInput menuOptionsInput;
	private MainMenuPanelUI mainMenuPanelUI;
	private OptionsManager optionsManager;

	private void Awake()
	{
		loopingCounter = GetComponent<LoopingCounter>();
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		mainMenuPanelUI = FindFirstObjectByType<MainMenuPanelUI>();
		optionsManager = FindFirstObjectByType<OptionsManager>();
		
		RegisterToListeners(true);
	}

	private void Start()
	{
		SetCounterRange();

		if(gameData != null && gameData.enteredStageSelection && gameData.twoPlayersMode)
		{
			loopingCounter.SetTo((int)OptionType.TwoPlayersMode + 1);
		}
	}

	private void SetCounterRange()
	{
		var numberOfOptions = optionsManager != null ? optionsManager.GetNumberOfOptions() : 0;
		var max = Mathf.Max(1, numberOfOptions);
		
		loopingCounter.SetRange(1, max);
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

			loopingCounter.valueChangedEvent.AddListener(OnCounterValueChanged);
		}
		else
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
			}

			loopingCounter.valueChangedEvent.RemoveListener(OnCounterValueChanged);
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		TriggerOnKeyPressed(() =>
		{
			if(direction == -1)
			{
				loopingCounter.DecreaseBy(1);
			}
			else if(direction == 1)
			{
				loopingCounter.IncreaseBy(1);
			}
		});
	}

	private void OnSubmitKeyPressed()
	{
		if(optionsManager != null)
		{
			TriggerOnKeyPressed(() => optionsManager.SubmitOption(GetOptionTypeByCounterValue()));
		}
	}

	private void TriggerOnKeyPressed(Action onKeyPressed)
	{
		if(mainMenuPanelUI == null)
		{
			return;
		}

		if(mainMenuPanelUI.ReachedTargetPosition())
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
			optionsManager.SelectOption(GetOptionTypeByCounterValue());
		}
	}

	private OptionType GetOptionTypeByCounterValue()
	{
		var index = loopingCounter.CurrentValue - 1;

		return (OptionType)index;
	}
}