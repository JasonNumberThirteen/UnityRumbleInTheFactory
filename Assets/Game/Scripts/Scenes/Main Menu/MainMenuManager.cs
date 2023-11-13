using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	public GameData gameData;
	public Timer timer;
	public MainMenuOptionsController mainMenuOptionsController;

	private void Start()
	{
		if(gameData.enteredStageSelection)
		{
			timer.InterruptTimer();
			SelectTwoPlayersModeOption();
		}
	}

	private void SelectTwoPlayersModeOption()
	{
		if(gameData.twoPlayersMode)
		{
			mainMenuOptionsController.IncreaseOptionValue();
			mainMenuOptionsController.SelectOption();
		}
	}
}