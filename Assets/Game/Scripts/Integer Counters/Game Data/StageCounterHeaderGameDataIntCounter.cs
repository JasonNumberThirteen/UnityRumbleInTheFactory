public class StageCounterHeaderGameDataIntCounter : GameDataIntCounter
{
	protected override int GetCounterValue() => gameData.StageNumber;
}