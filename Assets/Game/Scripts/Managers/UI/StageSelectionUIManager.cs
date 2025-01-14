using UnityEngine;

public class StageSelectionUIManager : UIManager
{
	[SerializeField] private GameData gameData;

	private StageCounterStageSelectionTextUI stageCounterTextUI;
	private NoStagesStageSelectionTextUI noStagesTextUI;

	private void Awake()
	{
		stageCounterTextUI = FindAnyObjectByType<StageCounterStageSelectionTextUI>();
		noStagesTextUI = FindAnyObjectByType<NoStagesStageSelectionTextUI>();
	}

	private void Start()
	{
		var noStagesFound = GameDataMethods.NoStagesFound(gameData);

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