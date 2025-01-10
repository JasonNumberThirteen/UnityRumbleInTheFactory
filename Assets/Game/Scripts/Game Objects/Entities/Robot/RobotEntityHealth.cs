using UnityEngine;

[RequireComponent(typeof(EntityExploder), typeof(RobotEntityRankController))]
public class RobotEntityHealth : MonoBehaviour
{
	public int CurrentHealth {get; protected set;}
	
	[SerializeField, Min(1)] private int initialHealth;

	protected StageSoundManager stageSoundManager;

	private EntityExploder entityExploder;
	private RobotEntityRankController robotEntityRankController;

	public virtual void TakeDamage(GameObject sender, int damage)
	{
		CurrentHealth -= damage;

		CheckHealth(sender);
	}

	protected virtual void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
		robotEntityRankController = GetComponent<RobotEntityRankController>();

		RegisterToListeners(true);
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

	private void OnRankChanged(RobotRank robotRank)
	{
		CurrentHealth = robotRank.GetHealth();
	}
}