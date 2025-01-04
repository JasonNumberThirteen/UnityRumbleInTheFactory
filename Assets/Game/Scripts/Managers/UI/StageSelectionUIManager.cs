using UnityEngine;

public class StageSelectionUIManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private StageCounterStageSelectionTextUI stageCounterTextUI;
	private NoStagesStageSelectionTextUI noStagesTextUI;

	private void Awake()
	{
		stageCounterTextUI = FindFirstObjectByType<StageCounterStageSelectionTextUI>();
		noStagesTextUI = FindFirstObjectByType<NoStagesStageSelectionTextUI>();
	}

	private void Start()
	{
		var noStagesFound = gameData != null && gameData.NoStagesFound();

		SetStageCounterTextUIActive(!noStagesFound);
		SetNoStagesTextUIActive(noStagesFound);
	}

	private void SetStageCounterTextUIActive(bool active)
	{
		if(stageCounterTextUI != null)
		{
			stageCounterTextUI.SetActive(active);
		}
	}

	private void SetNoStagesTextUIActive(bool active)
	{
		if(noStagesTextUI != null)
		{
			noStagesTextUI.SetActive(active);
		}
	}
}