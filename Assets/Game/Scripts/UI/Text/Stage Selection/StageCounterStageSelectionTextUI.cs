using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterStageSelectionTextUI : StageSelectionTextUI
{
	[SerializeField] private GameData gameData;
	
	private StageSelectionManager stageSelectionManager;
	private LoopingIntCounter loopingIntCounter;

	public int GetCurrentCounterValue() => loopingIntCounter.CurrentValue;

	protected override void Awake()
	{
		base.Awake();
		
		stageSelectionManager = FindAnyObjectByType<StageSelectionManager>();
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
				stageSelectionManager.navigationDirectionChangedEvent.AddListener(OnNavigationDirectionChanged);
			}
		}
		else
		{
			if(stageSelectionManager != null)
			{
				stageSelectionManager.navigationDirectionChangedEvent.RemoveListener(OnNavigationDirectionChanged);
			}
		}
	}

	private void OnNavigationDirectionChanged(int navigationDirection)
	{
		if(navigationDirection > 0)
		{
			loopingIntCounter.IncreaseBy(navigationDirection);
		}
		else if(navigationDirection < 0)
		{
			loopingIntCounter.DecreaseBy(Mathf.Abs(navigationDirection));
		}
	}

	private void Start()
	{
		if(gameData != null && gameData.StagesData != null && gameData.StagesData.Length > 0)
		{
			loopingIntCounter.SetRange(1, gameData.StagesData.Length);
		}
	}
}