using UnityEngine;

public class PlayerRobotRank : MonoBehaviour
{
	public Rank CurrentRank {get; private set;}
	
	public PlayerData data;
	public Rank[] ranks;
	
	public void Promote()
	{
		++data.Rank;

		SetRank();
	}
	
	public void SetRank()
	{
		CurrentRank = ranks[data.Rank - 1];

		SetHealth();
		SetMovementSpeed();
	}

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