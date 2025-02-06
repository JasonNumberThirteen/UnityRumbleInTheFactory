using UnityEngine;
using UnityEngine.Events;

public class RobotEntityRankController : MonoBehaviour
{
	public UnityEvent<RobotRank> rankChangedEvent;
	
	[SerializeField] protected RobotData robotData;

	[SerializeField] private bool resetRankOnStart = true;
	
	protected int rankNumber = 1;

	public RobotRank GetCurrentRobotRank() => robotData != null ? robotData.GetRankByIndex(rankNumber - 1) : null;

	public void IncreaseRankBy(int ranks)
	{
		SetRankNumber(rankNumber + ranks);
	}

	private void Start()
	{
		if(resetRankOnStart)
		{
			SetRankNumber(1, true);
		}
		else
		{
			rankChangedEvent?.Invoke(GetCurrentRobotRank());
		}
	}

	private void SetRankNumber(int newRankNumber, bool forceInvokingEvent = false)
	{
		var previousRankNumber = rankNumber;

		rankNumber = Mathf.Clamp(newRankNumber, 1, robotData != null ? robotData.GetNumberOfRanks() : 1);

		if(forceInvokingEvent || previousRankNumber != rankNumber)
		{
			rankChangedEvent?.Invoke(GetCurrentRobotRank());
		}
	}
}