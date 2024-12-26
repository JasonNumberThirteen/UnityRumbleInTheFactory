using TMPro;
using UnityEngine;

public class StageSelectionUIManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private TextMeshProUGUI stageCounterTextUI;
	[SerializeField] private TextMeshProUGUI noStagesMessageTextUI;

	private void Start()
	{
		var foundAnyStage = gameData != null && !gameData.StagesDoNotExist();
		
		stageCounterTextUI.enabled = foundAnyStage;
		noStagesMessageTextUI.enabled = !foundAnyStage;
	}
}