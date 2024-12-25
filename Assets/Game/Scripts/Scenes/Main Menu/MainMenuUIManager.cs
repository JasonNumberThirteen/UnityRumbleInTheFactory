using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	public MainMenuCounter[] counters;

	[SerializeField] private GameData gameData;
	[SerializeField] private Timer mainMenuPanelTimer;

	private void Start()
	{
		SetCounterValues();

		if(gameData != null && mainMenuPanelTimer != null && gameData.enteredStageSelection)
		{
			mainMenuPanelTimer.InterruptTimer();
		}
	}
	
	private void SetCounterValues()
	{
		foreach (MainMenuCounter mmc in counters)
		{
			mmc.SetCounterValue();
		}
	}
}

[System.Serializable]
public class MainMenuCounter
{
	[SerializeField] private MainMenuData data;
	[SerializeField] private IntCounter counter;

	public void SetCounterValue() => counter.SetTo(data.MainMenuCounterValue());
}