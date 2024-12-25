using System;
using UnityEngine;

[RequireComponent(typeof(LoopingCounter))]
public class MainMenuOptionsController : MonoBehaviour
{
	[SerializeField] private Option[] options;
	[SerializeField] private GameData gameData;

	private LoopingCounter loopingCounter;
	private MenuOptionsInput menuOptionsInput;
	private MainMenuPanelUI mainMenuPanelUI;

	public void SelectOption()
	{
		var currentOption = GetCurrentOption();

		if(currentOption != null)
		{
			currentOption.Select();
		}
	}

	public void SubmitOption()
	{
		var currentOption = GetCurrentOption();

		if(currentOption != null)
		{
			currentOption.Submit();
		}
	}

	private Option GetCurrentOption() => options[loopingCounter.CurrentValue - 1];

	private void Awake()
	{
		loopingCounter = GetComponent<LoopingCounter>();
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		mainMenuPanelUI = FindFirstObjectByType<MainMenuPanelUI>();
		
		loopingCounter.SetRange(1, Mathf.Max(1, options.Length));
		RegisterToListeners(true);
	}

	private void Start()
	{
		if(gameData != null && gameData.enteredStageSelection && gameData.twoPlayersMode)
		{
			loopingCounter.SetTo(2);
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
		TriggerOnKeyPressed(SubmitOption);
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
		SelectOption();
	}
}