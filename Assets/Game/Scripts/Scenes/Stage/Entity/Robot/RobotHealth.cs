using UnityEngine;

public class RobotHealth : MonoBehaviour
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
		EntityExploder ee = GetComponent<EntityExploder>();

		if(ee != null)
		{
			ee.Explode();
		}
	}
}