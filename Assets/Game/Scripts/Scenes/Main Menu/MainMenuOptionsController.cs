using UnityEngine;

[RequireComponent(typeof(LoopingCounter))]
public class MainMenuOptionsController : MonoBehaviour
{
	[SerializeField] private Option[] options;
	[SerializeField] private GameData gameData;
	[SerializeField] private Timer mainMenuPanelUITimer;

	private LoopingCounter loopingCounter;
	private MenuOptionsInput menuOptionsInput;

	public void SelectOption()
	{
		var currentOption = GetCurrentOption();

		if(currentOption != null)
		{
			currentOption.onSelect.Invoke();
		}
	}

	public void SubmitOption()
	{
		var currentOption = GetCurrentOption();

		if(currentOption != null)
		{
			currentOption.onSubmit.Invoke();
		}
	}

	private Option GetCurrentOption() => options[loopingCounter.CurrentValue - 1];

	private void Awake()
	{
		loopingCounter = GetComponent<LoopingCounter>();
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		
		SetLoopingCounterRange();
		RegisterToListeners(true);
	}

	private void Start()
	{
		if(gameData != null && gameData.enteredStageSelection && gameData.twoPlayersMode)
		{
			loopingCounter.SetTo(2);
			SelectOption();
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
		}
		else
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
			}
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(mainMenuPanelUITimer == null)
		{
			return;
		}
		
		if(mainMenuPanelUITimer.Finished)
		{
			if(direction == -1)
			{
				loopingCounter.DecreaseBy(1);
				SelectOption();
			}
			else if(direction == 1)
			{
				loopingCounter.IncreaseBy(1);
				SelectOption();
			}
		}
		else
		{
			mainMenuPanelUITimer.InterruptTimer();
		}
	}

	private void OnSubmitKeyPressed()
	{
		if(mainMenuPanelUITimer == null)
		{
			return;
		}
		
		if(mainMenuPanelUITimer.Finished)
		{
			SubmitOption();
		}
		else
		{
			mainMenuPanelUITimer.InterruptTimer();
		}
	}

	private void SetLoopingCounterRange()
	{
		loopingCounter.min = 1;
		loopingCounter.max = Mathf.Max(1, options.Length);
	}
}