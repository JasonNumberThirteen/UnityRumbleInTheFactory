using UnityEngine;

public class PlayerLivesCounterPanelUI : CounterPanelUI
{
	[SerializeField] private PlayerData playerData;

	public void UpdateCounterIfPossible()
	{
		if(playerData != null && intCounter != null)
		{
			intCounter.SetTo(playerData.Lives);
		}
	}

	private void Start()
	{
		UpdateCounterIfPossible();
	}
}