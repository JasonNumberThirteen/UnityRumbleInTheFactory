using UnityEngine;

public class MainMenuOptionsController : MonoBehaviour
{
	public Option[] options;
	public LoopingCounter counter;
	public GameData gameData;

	public void SelectOption() => CurrentOption().onSelect.Invoke();
	public void SubmitOption() => CurrentOption().onSubmit.Invoke();
	public void IncreaseOptionValue() => counter.IncreaseBy(1);
	public void DecreaseOptionValue() => counter.DecreaseBy(1);

	private void Awake() => SetCounterRange();
	private Option CurrentOption() => options[CounterValueIndex()];
	private int CounterValueIndex() => counter.CurrentValue - 1;

	private void Start()
	{
		if(gameData != null && gameData.enteredStageSelection && gameData.twoPlayersMode)
		{
			counter.SetTo(2);
			SelectOption();
		}
	}

	private void SetCounterRange()
	{
		counter.min = 1;
		counter.max = options.Length;
	}
}