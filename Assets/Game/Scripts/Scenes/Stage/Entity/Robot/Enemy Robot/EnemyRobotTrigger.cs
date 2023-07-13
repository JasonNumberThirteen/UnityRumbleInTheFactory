public class EnemyRobotTrigger : RobotTrigger
{
	public PlayerData playerData;
	
	public override void TriggerEffect()
	{
		EnemyRobotPoints erp = GetComponent<EnemyRobotPoints>();

		playerData.score += erp.pointsForDestruction;

		base.TriggerEffect();
	}
}