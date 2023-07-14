public class EnemyRobotHealth : RobotHealth
{
	public PlayerData playerData;
	
	protected override void Explode()
	{
		EnemyRobotPoints erp = GetComponent<EnemyRobotPoints>();

		playerData.Score += erp.pointsForDestruction;

		StageManager.instance.uiManager.CreateGainedPointsCounter(gameObject.transform.position, erp.pointsForDestruction);
		base.Explode();
	}
}