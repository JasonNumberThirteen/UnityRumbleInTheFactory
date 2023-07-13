public class LifeBonusEffect : BonusEffect
{
	public PlayerData playerData;
	
	public override void PerformEffect() => ++playerData.lives;
}