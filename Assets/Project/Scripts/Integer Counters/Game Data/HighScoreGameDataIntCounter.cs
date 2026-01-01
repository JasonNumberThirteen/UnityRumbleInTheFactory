public class HighScoreGameDataIntCounter : GameDataIntCounter
{
	protected override int GetCounterValue() => gameData.HighScore;
}