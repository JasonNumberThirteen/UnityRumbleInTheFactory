using UnityEngine;
using UnityEngine.Events;

public class RobotEntityRankController : MonoBehaviour
{
	[SerializeField] protected RobotData robotData;
	
	public UnityEvent<RobotRank> rankChangedEvent;

	public RobotData GetRobotData() => robotData;

	public void IncreaseRank()
	{
		if(robotData == null)
		{
			return;
		}

		++robotData.RankNumber;
		
		rankChangedEvent?.Invoke(robotData.GetRank());
	}

	protected virtual void Start()
	{
		if(robotData != null)
		{
			robotData.RankNumber = 1;

			rankChangedEvent?.Invoke(robotData.GetRank());
		}
	}
}