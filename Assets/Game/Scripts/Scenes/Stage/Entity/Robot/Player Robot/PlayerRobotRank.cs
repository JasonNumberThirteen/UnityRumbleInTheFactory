using UnityEngine;

public class PlayerRobotRank : MonoBehaviour
{
	public Rank CurrentRank {get; private set;}
	
	public PlayerData data;
	public Rank[] ranks;
	
	public void SetRank() => CurrentRank = ranks[data.Rank - 1];
}

[System.Serializable]
public class Rank
{
	[Min(1)] public int damage;
	[Min(1)] public int bulletLimit;
	[Min(0.01f)] public float movementSpeed;
	[Min(0.01f)] public float bulletSpeed;
	public bool canDestroyMetal;
}