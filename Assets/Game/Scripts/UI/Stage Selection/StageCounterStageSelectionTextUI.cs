using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterStageSelectionTextUI : StageSelectionTextUI
{
	[SerializeField] private GameData gameData;
	
	private LoopingIntCounter loopingIntCounter;

	public int GetCurrentCounterValue() => loopingIntCounter.CurrentValue;

	public void ModifyCounterBy(int value)
	{
		if(value > 0)
		{
			loopingIntCounter.IncreaseBy(value);
		}
		else if(value < 0)
		{
			loopingIntCounter.DecreaseBy(Mathf.Abs(value));
		}
	}

	protected override void Awake()
	{
		base.Awake();
		
		loopingIntCounter = GetComponent<LoopingIntCounter>();
	}

	private void Start()
	{
		if(gameData != null && gameData.StagesData != null && gameData.StagesData.Length > 0)
		{
			loopingIntCounter.SetRange(1, gameData.StagesData.Length);
		}
	}
}