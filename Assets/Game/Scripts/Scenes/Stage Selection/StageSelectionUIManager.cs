using TMPro;
using UnityEngine;

public class StageSelectionUIManager : MonoBehaviour
{
	public GameData gameData;
	public TextMeshProUGUI stageCounter, noStagesMessage;

	private void Start()
	{
		bool foundAnyStage = !gameData.StagesDoNotExist();
		
		stageCounter.enabled = foundAnyStage;
		noStagesMessage.enabled = !foundAnyStage;
	}
}