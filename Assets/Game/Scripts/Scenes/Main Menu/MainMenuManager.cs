using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	public GameData gameData;
	public Timer timer;

	private void Start()
	{
		if(gameData.enteredStageSelection)
		{
			timer.InterruptTimer();
		}
	}
}