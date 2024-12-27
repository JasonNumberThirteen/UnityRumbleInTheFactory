using UnityEngine;

public class HighScoreIntCounter : IntCounter
{
	[SerializeField] private GameData gameData;

	private void Start()
	{
		if(gameData != null)
		{
			SetTo(gameData.highScore);
		}
	}
}