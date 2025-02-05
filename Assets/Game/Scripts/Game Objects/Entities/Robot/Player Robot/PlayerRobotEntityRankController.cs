public class PlayerRobotEntityRankController : RobotEntityRankController
{
	private void Awake()
	{
		if(robotData != null && robotData is PlayerRobotData playerRobotData)
		{
			rankNumber = playerRobotData.RankNumber;
		}

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
			rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnRankChanged(RobotRank robotRank)
	{
		if(robotData != null && robotData is PlayerRobotData playerRobotData)
		{
			playerRobotData.RankNumber = rankNumber;
		}
	}
}