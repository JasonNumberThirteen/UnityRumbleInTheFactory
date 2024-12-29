using UnityEngine;

public class StageSelectionUIManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private StageSelectionStageCounterTextUI stageSelectionStageCounterTextUI;
	private NoStagesStageSelectionTextUI stageSelectionNoStagesTextUI;

	private void Awake()
	{
		stageSelectionStageCounterTextUI = FindFirstObjectByType<StageSelectionStageCounterTextUI>();
		stageSelectionNoStagesTextUI = FindFirstObjectByType<NoStagesStageSelectionTextUI>();
	}

	private void Start()
	{
		var foundAnyStage = gameData != null && !gameData.NoStagesFound();

		SetStageCounterTextUIActive(foundAnyStage);
		SetNoStagesMessageTextUIActive(!foundAnyStage);
	}

	private void SetStageCounterTextUIActive(bool active)
	{
		if(stageSelectionStageCounterTextUI != null)
		{
			stageSelectionStageCounterTextUI.SetActive(active);
		}
	}

	private void SetNoStagesMessageTextUIActive(bool active)
	{
		if(stageSelectionNoStagesTextUI != null)
		{
			stageSelectionNoStagesTextUI.SetActive(active);
		}
	}
}