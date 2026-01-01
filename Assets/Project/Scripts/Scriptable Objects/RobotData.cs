using UnityEngine;

public abstract class RobotData : ScriptableObject
{
	[SerializeField, Min(1)] private int ordinalNumber;
	
	public abstract RobotRank GetRankByIndex(int index);
	public abstract int GetNumberOfRanks();

	public int GetOrdinalNumber() => ordinalNumber;
}