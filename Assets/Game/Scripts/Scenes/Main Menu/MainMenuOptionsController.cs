using UnityEngine;

public class MainMenuOptionsController : MonoBehaviour
{
	public Option[] options;
	public LoopingCounter counter;
	public GameData gameData;
	public Timer mainMenuPanelTimer;

	private MenuOptionsInput menuOptionsInput;

	public void SelectOption() => CurrentOption().onSelect.Invoke();
	public void SubmitOption() => CurrentOption().onSubmit.Invoke();
	public void IncreaseOptionValue() => counter.IncreaseBy(1);
	public void DecreaseOptionValue() => counter.DecreaseBy(1);

	private Option CurrentOption() => options[CounterValueIndex()];
	private int CounterValueIndex() => counter.CurrentValue - 1;

	private void Awake()
	{
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		
		SetCounterRange();
		RegisterToListeners(true);
	}

	private void Start()
	{
		if(gameData != null && gameData.enteredStageSelection && gameData.twoPlayersMode)
		{
			counter.SetTo(2);
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
		if(mainMenuPanelTimer.Finished)
		{
			if(direction == -1)
			{
				DecreaseOptionValue();
				SelectOption();
			}
			else if(direction == 1)
			{
				IncreaseOptionValue();
				SelectOption();
			}
		}
		else
		{
			mainMenuPanelTimer.InterruptTimer();
		}
	}

	private void OnSubmitKeyPressed()
	{
		if(mainMenuPanelTimer.Finished)
		{
			SubmitOption();
		}
		else
		{
			mainMenuPanelTimer.InterruptTimer();
		}
	}

	private void SetCounterRange()
	{
		counter.min = 1;
		counter.max = options.Length;
	}
}