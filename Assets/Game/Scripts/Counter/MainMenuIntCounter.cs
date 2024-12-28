using UnityEngine;

public class MainMenuIntCounter : IntCounter
{
	[SerializeField] private MainMenuData mainMenuData;

	private void Start()
	{
		if(mainMenuData != null)
		{
			SetTo(mainMenuData.GetMainMenuCounterValue());
		}
	}
}