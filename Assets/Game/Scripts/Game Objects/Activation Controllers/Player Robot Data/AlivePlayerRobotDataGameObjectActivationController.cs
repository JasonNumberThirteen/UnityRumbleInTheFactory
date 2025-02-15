public class AlivePlayerRobotDataGameObjectActivationController : PlayerRobotDataGameObjectActivationController
{
	protected override bool GOShouldBeActive() => base.GOShouldBeActive() && playerRobotData.IsAlive;
}