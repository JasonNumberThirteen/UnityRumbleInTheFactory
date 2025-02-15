public class PreviousHighScoreGameDataIntCounter : GameDataIntCounter
{
	protected override int GetCounterValue() => gameData.PreviousHighScore;
}