using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	public MainMenuCounter[] counters;

	private void Start() => SetCounterValues();
	
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
	[SerializeField] private Counter counter;

	public void SetCounterValue() => counter.SetTo(data.MainMenuCounterValue());
}