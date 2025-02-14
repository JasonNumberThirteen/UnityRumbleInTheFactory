using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterHeaderStageSelectionTextUI : StageSelectionTextUI
{
	private StageSelectionOptionSelectionManager stageSelectionOptionSelectionManager;
	private LoopingIntCounter loopingIntCounter;

	public int GetCurrentCounterValue() => loopingIntCounter.CurrentValue;

	protected override void Awake()
	{
		base.Awake();
		
		stageSelectionOptionSelectionManager = ObjectMethods.FindComponentOfType<StageSelectionOptionSelectionManager>();
		loopingIntCounter = GetComponent<LoopingIntCounter>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageSelectionOptionSelectionManager != null)
			{
				stageSelectionOptionSelectionManager.navigationDirectionChangedEvent.AddListener(loopingIntCounter.ModifyBy);
			}
		}
		else
		{
			if(stageSelectionOptionSelectionManager != null)
			{
				stageSelectionOptionSelectionManager.navigationDirectionChangedEvent.RemoveListener(loopingIntCounter.ModifyBy);
			}
		}
	}
}