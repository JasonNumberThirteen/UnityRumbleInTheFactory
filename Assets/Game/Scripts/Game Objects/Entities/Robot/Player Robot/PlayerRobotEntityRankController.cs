using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntity))]
public class PlayerRobotEntityRankController : RobotEntityRankController
{
	public RobotRank CurrentRank {get; private set;}

	private PlayerRobotEntity playerRobotEntity;
	
	public override void IncreaseRank()
	{
		base.IncreaseRank();
		UpdateCurrentRankIfPossible();
	}

	private void Awake()
	{
		playerRobotEntity = GetComponent<PlayerRobotEntity>();
	}

	private void Start()
	{
		UpdateCurrentRankIfPossible();
	}

	private void UpdateCurrentRankIfPossible()
	{
		var playerRobotData = playerRobotEntity.GetPlayerRobotData();

		if(playerRobotData != null)
		{
			CurrentRank = playerRobotData.GetRank();

			rankChangedEvent?.Invoke(CurrentRank);
		}
	}
}