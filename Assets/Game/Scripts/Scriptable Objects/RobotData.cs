using UnityEngine;

public abstract class RobotData : ScriptableObject
{
	public abstract RobotRank GetRankByIndex(int index);
	public abstract int GetNumberOfRanks();
}