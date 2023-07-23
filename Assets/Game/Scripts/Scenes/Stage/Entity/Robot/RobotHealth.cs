using UnityEngine;

public class RobotHealth : MonoBehaviour
{
	[Min(1)] public int initialHealth = 1;

	public int CurrentHealth {get; set;}

	public virtual void ReceiveDamage(int damage)
	{
		CurrentHealth -= damage;

		CheckHealth();
	}

	protected virtual void Start() => CurrentHealth = initialHealth;

	protected virtual void CheckHealth()
	{
		if(CurrentHealth <= 0)
		{
			Explode();
		}
	}

	protected virtual void Explode()
	{
		EntityExploder ee = GetComponent<EntityExploder>();

		if(ee != null)
		{
			ee.Explode();
		}
	}
}