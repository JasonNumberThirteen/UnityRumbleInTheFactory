using UnityEngine;

public class StageIntCounterPanelUI : IntCounterPanelUI
{
	[SerializeField] private GameData gameData;

	private void Start()
	{
		if(GameDataMethods.GameDataIsDefined(gameData))
		{
			SetValueToCounter(gameData.StageNumber);
		}
	}
}