using UnityEngine;
using UnityEngine.Events;

public class RobotEntityRankController : MonoBehaviour
{
	[SerializeField] private RobotData robotData;
	
	public UnityEvent<RobotRank> rankChangedEvent;

	public void IncreaseRank()
	{
		if(robotData == null)
		{
			return;
		}

		++robotData.RankNumber;
		
		rankChangedEvent?.Invoke(robotData.GetRank());
	}
}