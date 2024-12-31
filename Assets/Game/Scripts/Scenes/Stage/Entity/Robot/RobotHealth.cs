using UnityEngine;

public class RobotHealth : MonoBehaviour, IUpgradeableByRobotRank
{
	[Min(1)] public int initialHealth = 1;

	public int Health {get; private set;}

	protected StageSoundManager stageSoundManager;

	public virtual void UpdateValuesUpgradeableByRobotRank(RobotRank robotRank) => Health = robotRank.GetHealth();

	public virtual void TakeDamage(GameObject sender, int damage)
	{
		Health -= damage;

		CheckHealth(sender);
	}

	protected virtual void Awake() => stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	protected virtual void Start() => Health = initialHealth;

	protected virtual void CheckHealth(GameObject sender)
	{
		if(Health <= 0)
		{
			Die(sender);
		}
		else if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.RobotDamage);
		}
	}

	protected virtual void Die(GameObject sender)
	{
		if(TryGetComponent(out EntityExploder ee))
		{
			ee.TriggerExplosion();
		}
	}
}