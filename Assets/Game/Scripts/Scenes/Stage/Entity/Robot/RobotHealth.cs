using UnityEngine;

public class RobotHealth : MonoBehaviour
{
	[Min(1)] public int initialHealth = 1;

	public int CurrentHealth {get; private set;}

	public void ReceiveDamage(int damage)
	{
		CurrentHealth -= damage;

		CheckHealth();
	}

	private void Start() => CurrentHealth = initialHealth;

	private void CheckHealth()
	{
		if(CurrentHealth <= 0)
		{
			Explode();
		}
	}

	private void Explode()
	{
		EntityExploder ee = GetComponent<EntityExploder>();

		if(ee != null)
		{
			ee.Explode();
		}
	}
}