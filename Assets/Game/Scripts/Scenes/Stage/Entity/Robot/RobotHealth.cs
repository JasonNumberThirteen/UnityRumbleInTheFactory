using UnityEngine;

public class RobotHealth : MonoBehaviour, IUpgradeable
{
	[Min(1)] public int initialHealth = 1;

	public int Health {get; private set;}

	private RobotAudioSource audioSource;

	public virtual void UpdateValues(Rank rank) => Health = rank.health;

	public virtual void TakeDamage(GameObject sender, int damage)
	{
		Health -= damage;

		CheckHealth(sender);
	}

	protected virtual void Awake() => audioSource = GetComponent<RobotAudioSource>();
	protected virtual void Start() => Health = initialHealth;

	protected virtual void CheckHealth(GameObject sender)
	{
		if(Health <= 0)
		{
			Die(sender);
		}
		else
		{
			audioSource.PlayDamageSound();
		}
	}

	protected virtual void Die(GameObject sender)
	{
		if(TryGetComponent(out EntityExploder ee))
		{
			ee.Explode();
		}
	}
}