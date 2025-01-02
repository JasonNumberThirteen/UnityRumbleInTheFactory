using UnityEngine;

[RequireComponent(typeof(EntityExploder))]
public class RobotHealth : MonoBehaviour, IUpgradeableByRobotRank
{
	public int CurrentHealth {get; private set;}
	
	[SerializeField, Min(1)] private int initialHealth;

	protected StageSoundManager stageSoundManager;

	private EntityExploder entityExploder;

	public virtual void UpdateValuesUpgradeableByRobotRank(RobotRank robotRank)
	{
		CurrentHealth = robotRank.GetHealth();
	}

	public virtual void TakeDamage(GameObject sender, int damage)
	{
		CurrentHealth -= damage;

		CheckHealth(sender);
	}

	protected virtual void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
	}

	protected virtual void Start()
	{
		CurrentHealth = initialHealth;
	}

	protected virtual void CheckHealth(GameObject sender)
	{
		if(CurrentHealth <= 0)
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
		entityExploder.TriggerExplosion();
	}
}