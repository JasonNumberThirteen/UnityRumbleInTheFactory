using UnityEngine;

[RequireComponent(typeof(LoopingIntCounter))]
public class StageCounterHeaderTextUIComponentSelector : MonoBehaviour
{
	[SerializeField] private StageCounterSceneType stageCounterSceneType;
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
		return stageCounterSceneType switch
		{
			StageCounterSceneType.StageSelection => gameObject.AddComponent<StageCounterHeaderStageSelectionTextUI>(),
			StageCounterSceneType.Stage => gameObject.AddComponent<StageCounterHeaderStageTextUI>(),
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