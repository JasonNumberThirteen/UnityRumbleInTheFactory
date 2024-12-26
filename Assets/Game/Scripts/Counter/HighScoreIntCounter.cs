using UnityEngine;

public class HighScoreIntCounter : IntCounter
{
	[SerializeField] private GameData gameData;

	protected override void Start()
	{
		base.Start();

		if(gameData != null)
		{
			SetTo(gameData.highScore);
		}
	}
}