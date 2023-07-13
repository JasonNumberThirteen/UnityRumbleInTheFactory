public class EnemyRobotHealth : RobotHealth
{
	public PlayerData playerData;
	
	protected override void Explode()
	{
		EnemyRobotPoints erp = GetComponent<EnemyRobotPoints>();

		playerData.score += erp.pointsForDestruction;

		StageManager.instance.uiManager.CreateGainedPointsText(gameObject.transform.position, erp.pointsForDestruction);
		base.Explode();
	}
}