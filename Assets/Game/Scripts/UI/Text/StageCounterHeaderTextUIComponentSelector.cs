using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterHeaderTextUIComponentSelector : MonoBehaviour
{
	[SerializeField] private StageCounterHeaderSceneType stageCounterHeaderSceneType;
	[SerializeField] private GameData gameData;

	private LoopingIntCounter loopingIntCounter;
	private Component component;

	private void Awake()
	{
		loopingIntCounter = GetComponent<LoopingIntCounter>();
		component = AddAndGetComponentDependingOnSceneType();
	}

	private Component AddAndGetComponentDependingOnSceneType()
	{
		return stageCounterHeaderSceneType switch
		{
			StageCounterHeaderSceneType.StageSelection => gameObject.AddComponent<StageCounterHeaderStageSelectionTextUI>(),
			StageCounterHeaderSceneType.Stage => gameObject.AddComponent<StageCounterHeaderStageTextUI>(),
			_ => null
		};
	}

	private void Start()
	{
		SetNumberOfStagesRangeToCounterIfPossible();
		SetStageNumberToGameDataIfNeeded();
		SetStageNumberToCounterIfNeeded();
		DestroySelfIfAddedComponent();
	}

	private void SetNumberOfStagesRangeToCounterIfPossible()
	{
		if(gameData != null && gameData.StagesData != null && gameData.StagesData.Length > 0)
		{
			loopingIntCounter.SetRange(1, gameData.StagesData.Length);
		}
	}

	private void SetStageNumberToGameDataIfNeeded()
	{
		if(gameData != null && gameData.StageNumber == 0 && loopingIntCounter.CurrentValue != gameData.StageNumber)
		{
			gameData.SetStageNumber(loopingIntCounter.CurrentValue);
		}
	}

	private void SetStageNumberToCounterIfNeeded()
	{
		if(gameData != null && gameData.StageNumber > 0)
		{
			loopingIntCounter.SetTo(gameData.StageNumber);
		}
	}

	private void DestroySelfIfAddedComponent()
	{
		if(component != null)
		{
			Destroy(this);
		}
	}
}