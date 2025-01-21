using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterHeaderStageSelectionTextUI : StageSelectionTextUI
{
	private StageSelectionManager stageSelectionManager;
	private LoopingIntCounter loopingIntCounter;

	public int GetCurrentCounterValue() => loopingIntCounter.CurrentValue;

	protected override void Awake()
	{
		base.Awake();
		
		stageSelectionManager = ObjectMethods.FindComponentOfType<StageSelectionManager>();
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
			if(stageSelectionManager != null)
			{
				stageSelectionManager.navigationDirectionChangedEvent.AddListener(loopingIntCounter.ModifyBy);
			}
		}
		else
		{
			if(stageSelectionManager != null)
			{
				stageSelectionManager.navigationDirectionChangedEvent.RemoveListener(loopingIntCounter.ModifyBy);
			}
		}
	}
}