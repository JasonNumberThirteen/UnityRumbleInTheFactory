using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterStageSelectionTextUI : StageSelectionTextUI
{
	[SerializeField] private GameData gameData;
	
	private LoopingIntCounter loopingCounter;

	public int GetCurrentCounterValue() => loopingCounter.CurrentValue;

	public void ModifyCounterBy(int value)
	{
		if(value > 0)
		{
			loopingCounter.IncreaseBy(value);
		}
		else if(value < 0)
		{
			loopingCounter.DecreaseBy(Mathf.Abs(value));
		}
	}

	protected override void Awake()
	{
		base.Awake();
		
		loopingCounter = GetComponent<LoopingIntCounter>();
	}

	private void Start()
	{
		if(gameData != null && gameData.StagesData != null && gameData.StagesData.Length > 0)
		{
			loopingCounter.SetRange(1, gameData.StagesData.Length);
		}
	}
}