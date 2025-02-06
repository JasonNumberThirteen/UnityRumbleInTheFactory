using UnityEngine;

[RequireComponent(typeof(EntityExploder), typeof(RobotEntityRankController))]
public class RobotEntityHealth : MonoBehaviour
{
	public int CurrentHealth {get; protected set;}

	[SerializeField] private SoundEffectType explosionSoundEffectType;

	private EntityExploder entityExploder;
	private RobotEntityRankController robotEntityRankController;
	private StageSoundManager stageSoundManager;

	public virtual void TakeDamage(GameObject sender, int damage)
	{
		CurrentHealth -= damage;

		CheckHealth(sender);
	}

	protected virtual void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		robotEntityRankController = GetComponent<RobotEntityRankController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();

		RegisterToListeners(true);
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
		PlaySoundOnDeath();
	}

	protected virtual void OnRankChanged(RobotRank robotRank)
	{
		if(robotRank != null)
		{
			CurrentHealth = robotRank.GetHealth();
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			robotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			robotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void PlaySoundOnDeath()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(explosionSoundEffectType);
		}
	}
}