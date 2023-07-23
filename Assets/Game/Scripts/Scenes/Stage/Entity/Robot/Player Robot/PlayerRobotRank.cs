using UnityEngine;

public class PlayerRobotRank : MonoBehaviour
{
	public Rank CurrentRank {get; private set;}
	
	public PlayerData data;
	public Rank[] ranks;

	private void Start() => SetRank();
	
	public void SetRank()
	{
		RobotHealth rh = GetComponent<RobotHealth>();
		EntityMovement em = GetComponent<EntityMovement>();
		
		CurrentRank = ranks[data.Rank - 1];

		if(rh != null)
		{
			rh.CurrentHealth = CurrentRank.health;
		}

		if(em != null)
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