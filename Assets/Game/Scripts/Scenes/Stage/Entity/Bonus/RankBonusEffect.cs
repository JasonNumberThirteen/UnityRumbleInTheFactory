public class RankBonusEffect : BonusEffect
{
	public PlayerData playerData;
	
	public override void PerformEffect() => ++playerData.rank;
}