using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterHeaderStageSelectionTextUI : StageSelectionTextUI
{
	private LoopingIntCounter loopingIntCounter;
	private StageSelectionManager stageSelectionManager;

	public int GetCurrentCounterValue() => loopingIntCounter.CurrentValue;

	protected override void Awake()
	{
		base.Awake();
		
		loopingIntCounter = GetComponent<LoopingIntCounter>();
		stageSelectionManager = ObjectMethods.FindComponentOfType<StageSelectionManager>();

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