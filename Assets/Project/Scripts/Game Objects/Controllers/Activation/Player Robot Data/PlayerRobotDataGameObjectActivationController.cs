using UnityEngine;

public abstract class PlayerRobotDataGameObjectActivationController : GameObjectActivationController
{
	[SerializeField] protected PlayerRobotData playerRobotData;

	protected override bool GOShouldBeActive() => GOActivationStateCanBeChanged();
	protected override bool GOActivationStateCanBeChanged() => playerRobotData != null;
}