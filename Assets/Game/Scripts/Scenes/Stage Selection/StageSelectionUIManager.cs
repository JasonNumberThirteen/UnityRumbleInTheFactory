using TMPro;
using UnityEngine;

public class StageSelectionUIManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private TextMeshProUGUI noStagesMessageTextUI;

	private StageSelectionStageCounterTextUI stageSelectionStageCounterTextUI;

	private void Awake()
	{
		stageSelectionStageCounterTextUI = FindFirstObjectByType<StageSelectionStageCounterTextUI>();
	}

	private void Start()
	{
		var foundAnyStage = gameData != null && !gameData.StagesDoNotExist();

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
		if(noStagesMessageTextUI != null)
		{
			noStagesMessageTextUI.enabled = active;
		}
	}
}