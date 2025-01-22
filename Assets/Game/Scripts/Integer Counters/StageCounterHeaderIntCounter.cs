using UnityEngine;

public class StageCounterHeaderIntCounter : IntCounter
{
	[SerializeField] private GameData gameData;

	private void Start()
	{
		if(gameData != null)
		{
			SetTo(gameData.StageNumber);
		}
	}
}