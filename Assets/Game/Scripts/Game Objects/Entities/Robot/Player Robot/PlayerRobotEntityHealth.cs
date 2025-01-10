using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityRankController))]
public class PlayerRobotEntityHealth : RobotEntityHealth
{
	private PlayerRobotEntityRankController playerRobotEntityRankController;

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityRankController = GetComponent<PlayerRobotEntityRankController>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			playerRobotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			playerRobotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnRankChanged(PlayerRobotRank playerRobotRank)
	{
		CurrentHealth = playerRobotRank.GetHealth();
	}
}