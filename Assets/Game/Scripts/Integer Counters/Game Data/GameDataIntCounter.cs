using UnityEngine;

public abstract class GameDataIntCounter : IntCounter
{
	[SerializeField] protected GameData gameData;

	protected abstract int GetCounterValue();

	private bool CounterValueCanBeChanged() => gameData != null;

	private void Start()
	{
		if(CounterValueCanBeChanged())
		{
			SetTo(GetCounterValue());
		}
	}
}