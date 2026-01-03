public class AliveOnCurrentStagePlayerRobotDataGameObjectActivationController : PlayerRobotDataGameObjectActivationController
{
	protected override bool GOShouldBeActive() => base.GOShouldBeActive() && playerRobotData.WasAliveOnCurrentStage;
	protected override bool GOActivationStateCanBeChanged() => playerRobotData != null;
}