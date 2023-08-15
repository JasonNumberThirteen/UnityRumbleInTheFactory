using UnityEngine;

public class RobotHealth : MonoBehaviour, IUpgradeable
{
	[Min(1)] public int initialHealth = 1;

	public int CurrentHealth
	{
		get
		{
			return health;
		}
		set
		{
			health = value;

			CheckHealth();
		}
	}

	private int health;

	public virtual void UpdateValues(Rank rank) => CurrentHealth = rank.health;
	protected virtual void Start() => CurrentHealth = initialHealth;

	protected virtual void CheckHealth()
	{
		if(CurrentHealth <= 0)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		if(TryGetComponent(out EntityExploder ee))
		{
			ee.Explode();
		}
	}
}