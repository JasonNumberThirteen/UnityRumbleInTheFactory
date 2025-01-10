using UnityEngine;
using UnityEngine.Events;

public class RobotEntityRankController : MonoBehaviour
{
	[SerializeField] private RobotData robotData;
	[SerializeField] private bool resetRankOnStart = true;
	
	public UnityEvent<RobotRank> rankChangedEvent;

	public RobotData GetRobotData() => robotData;

	public void IncreaseRankBy(int ranks)
	{
		if(robotData != null)
		{
			SetRankNumber(robotData.RankNumber + ranks);
		}
	}

	private void Start()
	{
		if(resetRankOnStart)
		{	
			SetRankNumber(1);
		}
		else if(robotData != null)
		{
			rankChangedEvent?.Invoke(robotData.GetRank());
		}
	}

	private void SetRankNumber(int rankNumber)
	{
		if(robotData == null)
		{
			return;
		}

		var previousRankNumber = robotData.RankNumber;

		robotData.RankNumber = rankNumber;

		if(previousRankNumber != robotData.RankNumber)
		{
			rankChangedEvent?.Invoke(robotData.GetRank());
		}
	}
}