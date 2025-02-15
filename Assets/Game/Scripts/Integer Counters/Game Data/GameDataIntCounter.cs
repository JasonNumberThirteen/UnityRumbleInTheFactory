using UnityEngine;

public abstract class GameDataIntCounter : IntCounter
{
	[SerializeField] protected GameData gameData;

	protected abstract int GetCounterValue();

	private void Start()
	{
		if(GameDataMethods.GameDataIsDefined(gameData))
		{
			SetTo(GetCounterValue());
		}
	}
}