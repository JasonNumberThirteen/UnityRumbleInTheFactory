public class EnemyRobotTrigger : RobotTrigger
{
	public PlayerData playerData;
	
	public override void TriggerEffect()
	{
		EnemyRobotPoints erp = GetComponent<EnemyRobotPoints>();

		playerData.score += erp.pointsForDestruction;

		StageManager.instance.uiManager.CreateGainedPointsText(gameObject.transform.position, erp.pointsForDestruction);
		base.TriggerEffect();
	}
}