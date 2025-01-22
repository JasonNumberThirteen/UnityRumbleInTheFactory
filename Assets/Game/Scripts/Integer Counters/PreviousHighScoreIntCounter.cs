using UnityEngine;

public class PreviousHighScoreIntCounter : IntCounter
{
	[SerializeField] private GameData gameData;

	private void Start()
	{
		if(gameData != null)
		{
			SetTo(gameData.PreviousHighScore);
		}
	}
}