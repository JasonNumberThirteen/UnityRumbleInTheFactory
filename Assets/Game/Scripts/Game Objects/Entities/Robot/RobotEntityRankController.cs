using UnityEngine;
using UnityEngine.Events;

public class RobotEntityRankController : MonoBehaviour
{
	public UnityEvent<RobotRank, bool> rankChangedEvent;
	
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
			SetRankNumber(1, true, true);
		}
		else
		{
			rankChangedEvent?.Invoke(GetCurrentRobotRank(), true);
		}
	}

	private void SetRankNumber(int newRankNumber, bool forceInvokingEvent = false, bool setOnStart = false)
	{
		var previousRankNumber = rankNumber;

		rankNumber = newRankNumber.GetClampedValue(1, robotData != null ? robotData.GetNumberOfRanks() : 1);

		if(forceInvokingEvent || previousRankNumber != rankNumber)
		{
			rankChangedEvent?.Invoke(GetCurrentRobotRank(), setOnStart);
		}
	}
}