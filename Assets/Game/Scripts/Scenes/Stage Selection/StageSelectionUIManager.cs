using TMPro;
using UnityEngine;

public class StageSelectionUIManager : MonoBehaviour
{
	public GameData gameData;
	public TextMeshProUGUI stageCounter, noStagesMessage;

	private void Start()
	{
		bool foundAnyStage = gameData.stages.Length > 0;
		
		stageCounter.enabled = foundAnyStage;
		noStagesMessage.enabled = !foundAnyStage;
	}
}