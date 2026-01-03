using UnityEngine;

public class StageSelectionUIManager : UIManager
{
	[SerializeField] private GameData gameData;

	private StageCounterHeaderStageSelectionTextUI stageCounterHeaderTextUI;
	private NoStagesStageSelectionTextUI noStagesStageSelectionTextUI;

	private void Awake()
	{
		stageCounterHeaderTextUI = ObjectMethods.FindComponentOfType<StageCounterHeaderStageSelectionTextUI>();
		noStagesStageSelectionTextUI = ObjectMethods.FindComponentOfType<NoStagesStageSelectionTextUI>();
	}

	private void Start()
	{
		var anyStageFound = GameDataMethods.AnyStageFound(gameData);

		SetStageCounterHeaderTextUIActive(anyStageFound);
		SetNoStagesTextUIActive(!anyStageFound);
	}

	private void SetStageCounterHeaderTextUIActive(bool active)
	{
		if(stageCounterHeaderTextUI != null)
		{
			stageCounterHeaderTextUI.SetActive(active);
		}
	}

	private void SetNoStagesTextUIActive(bool active)
	{
		if(noStagesStageSelectionTextUI != null)
		{
			noStagesStageSelectionTextUI.SetActive(active);
		}
	}
}