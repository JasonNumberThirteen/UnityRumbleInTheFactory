using System;

public class PlayerRobotEntityRankController : RobotEntityRankController
{
	private void Awake()
	{
		OperateOnPlayerRobotDataIfPossible(playerRobotData => rankNumber = playerRobotData.RankNumber);
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
			rankWasChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			rankWasChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnRankChanged(RobotRank robotRank, bool setOnStart)
	{
		OperateOnPlayerRobotDataIfPossible(playerRobotData => playerRobotData.RankNumber = rankNumber);
	}

	private void OperateOnPlayerRobotDataIfPossible(Action<PlayerRobotData> action)
	{
		if(robotData != null && robotData is PlayerRobotData playerRobotData)
		{
			action?.Invoke(playerRobotData);
		}
	}
}