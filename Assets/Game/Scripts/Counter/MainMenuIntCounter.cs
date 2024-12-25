using UnityEngine;

public class MainMenuIntCounter : IntCounter
{
	[SerializeField] private MainMenuData mainMenuData;

	protected override void Start()
	{
		base.Start();

		if(mainMenuData != null)
		{
			SetTo(mainMenuData.MainMenuCounterValue());
		}
	}
}