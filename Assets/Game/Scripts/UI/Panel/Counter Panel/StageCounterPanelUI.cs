using UnityEngine;

public class StageCounterPanelUI : CounterPanelUI
{
	[SerializeField] private GameData gameData;

	private void Start()
	{
		if(GameDataMethods.GameDataIsDefined(gameData) && intCounter != null)
		{
			intCounter.SetTo(gameData.StageNumber);
		}
	}
}