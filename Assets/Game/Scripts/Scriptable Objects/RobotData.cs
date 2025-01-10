using UnityEngine;

public abstract class RobotData<T> : ScriptableObject where T : RobotRank
{
	public int RankNumber
	{
		get => rankNumber;
		set => rankNumber = Mathf.Clamp(value, 1, ranks != null && ranks.Length > 0 ? ranks.Length : 1);
	}

	[SerializeField] private T[] ranks;

	private int rankNumber;

	public T GetRank() => ranks[RankNumber - 1];
}