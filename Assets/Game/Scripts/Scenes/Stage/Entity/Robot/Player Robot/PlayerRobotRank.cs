using UnityEngine;

public class PlayerRobotRank : MonoBehaviour
{
	public Rank CurrentRank {get; private set;}
	
	public Rank[] ranks;

	private PlayerRobotData data;
	
	public void Promote()
	{
		++data.Data.Rank;

		SetRank();
	}
	
	public void SetRank()
	{
		CurrentRank = ranks[data.Data.Rank - 1];

		SetHealth();
		SetMovementSpeed();
	}

	private void Awake() => data = GetComponent<PlayerRobotData>();
	private void Start() => SetRank();

	private void SetHealth()
	{
		if(TryGetComponent(out RobotHealth rh))
		{
			rh.CurrentHealth = CurrentRank.health;
		}
	}

	private void SetMovementSpeed()
	{
		if(TryGetComponent(out EntityMovement em))
		{
			em.movementSpeed = CurrentRank.movementSpeed;
		}
	}
}

[System.Serializable]
public class Rank
{
	[Min(1)] public int health;
	[Min(1)] public int damage;
	[Min(1)] public int bulletLimit;
	[Min(0.01f)] public float movementSpeed;
	[Min(0.01f)] public float bulletSpeed;
	public bool canDestroyMetal;
}