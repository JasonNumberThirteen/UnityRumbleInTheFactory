using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer), typeof(LoopingIntCounter))]
public class MainMenuOptionSelectionManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private Timer timer;
	private LoopingIntCounter loopingIntCounter;
	private int navigationDirection;
	private MenuOptionsInput menuOptionsInput;
	private MainMenuPanelUI mainMenuPanelUI;
	private OptionsManager optionsManager;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		loopingIntCounter = GetComponent<LoopingIntCounter>();
		menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();
		mainMenuPanelUI = ObjectMethods.FindComponentOfType<MainMenuPanelUI>();
		optionsManager = ObjectMethods.FindComponentOfType<OptionsManager>();
		
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
			StartCoroutine(SelectTwoPlayersMode());
		}
	}

	private IEnumerator SelectTwoPlayersMode()
	{
		yield return new WaitForEndOfFrame();

		loopingIntCounter.SetTo((int)OptionType.TwoPlayersMode);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(timer.StartTimer);
			loopingIntCounter.valueChangedEvent.AddListener(OnCounterValueChanged);
			
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
			}
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(timer.StartTimer);
			loopingIntCounter.valueChangedEvent.RemoveListener(OnCounterValueChanged);
			
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
			}
		}
	}

	private void OnTimerStarted()
	{
		TriggerOnKeyPressed(() => loopingIntCounter.ModifyBy(navigationDirection));
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
		navigationDirection = direction;
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

	private void Update()
	{
		if(navigationDirection != 0 && !timer.TimerWasStarted)
		{
			timer.StartTimer();
		}
		else if(navigationDirection == 0)
		{
			timer.InterruptTimerIfPossible();
		}
	}

	private OptionType GetOptionTypeByCounterValue() => (OptionType)loopingIntCounter.CurrentValue;
}