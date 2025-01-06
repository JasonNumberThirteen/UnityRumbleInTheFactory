using UnityEngine;

public class StageCounterPanelUI : CounterPanelUI
{
	[SerializeField] private GameData gameData;

	private void Start()
	{
		if(gameData != null && intCounter != null)
		{
			intCounter.SetTo(gameData.StageNumber);
		}
	}
}