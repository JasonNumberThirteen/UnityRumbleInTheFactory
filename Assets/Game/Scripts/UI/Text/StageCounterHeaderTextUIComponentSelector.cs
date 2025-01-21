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
		SetStageNumberValueToCounter();
		DestroySelfIfAddedComponent();
	}

	private void SetNumberOfStagesRangeToCounterIfPossible()
	{
		if(gameData != null && gameData.StagesData != null && gameData.StagesData.Length > 0)
		{
			loopingIntCounter.SetRange(1, gameData.StagesData.Length);
		}
	}

	private void SetStageNumberValueToCounter()
	{
		if(gameData != null)
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