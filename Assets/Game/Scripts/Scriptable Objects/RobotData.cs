using UnityEngine;

public abstract class RobotData : ScriptableObject
{
	public abstract int RankNumber {get; set;}

	public abstract RobotRank GetRank();
}