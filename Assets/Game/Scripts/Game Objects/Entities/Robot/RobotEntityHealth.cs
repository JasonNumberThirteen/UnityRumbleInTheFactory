using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityExploder), typeof(RobotEntityRankController))]
public class RobotEntityHealth : MonoBehaviour
{
	public UnityEvent<int> currentHealthValueWasChangedEvent;

	[SerializeField] private SoundEffectType explosionSoundEffectType;

	private EntityExploder entityExploder;
	private RobotEntityRankController robotEntityRankController;
	private StageSoundManager stageSoundManager;
	private int currentHealth;

	public virtual void TakeDamage(GameObject sender, int damage)
	{
		ModifyCurrentHealthBy(-damage);

		if(currentHealth <= 0)
		{
			Die(sender);
		}
		else if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.RobotDamage);
		}
	}

	public void ModifyCurrentHealthBy(int value)
	{
		SetCurrentHealth(currentHealth + value);
	}

	protected virtual void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		robotEntityRankController = GetComponent<RobotEntityRankController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();

		RegisterToListeners(true);
	}

	protected virtual void Die(GameObject sender)
	{
		entityExploder.TriggerExplosion();
		PlaySoundOnDeath();
	}

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			robotEntityRankController.rankWasChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			robotEntityRankController.rankWasChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	protected virtual void OnRankChanged(RobotRank robotRank, bool setOnStart)
	{
		if(robotRank != null)
		{
			SetCurrentHealth(robotRank.GetHealth());
		}
	}

	protected void SetCurrentHealth(int newCurrentHealth)
	{
		var previousHealth = currentHealth;

		currentHealth = newCurrentHealth;

		if(previousHealth != currentHealth)
		{
			currentHealthValueWasChangedEvent?.Invoke(currentHealth);
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void PlaySoundOnDeath()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(explosionSoundEffectType);
		}
	}
}