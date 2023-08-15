using UnityEngine;

public class RobotHealth : MonoBehaviour, IUpgradeable
{
	[Min(1)] public int initialHealth = 1;

	public int Health {get; private set;}

	public virtual void UpdateValues(Rank rank) => Health = rank.health;

	public virtual void TakeDamage(int damage)
	{
		Health -= damage;

		CheckHealth();
	}

	protected virtual void Start() => Health = initialHealth;

	protected virtual void CheckHealth()
	{
		if(Health <= 0)
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