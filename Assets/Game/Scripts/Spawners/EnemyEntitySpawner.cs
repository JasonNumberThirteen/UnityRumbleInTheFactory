using UnityEngine;

[RequireComponent(typeof(Telefragger))]
public class EnemyEntitySpawner : EntitySpawner
{
	public bool IsBonus {get; set;}

	private Telefragger telefragger;

	protected override void Awake()
	{
		base.Awake();

		telefragger = GetComponent<Telefragger>();
	}

	protected override GameObject GetEntityInstance()
	{
		GameObject instance = base.GetEntityInstance();

		AddBonusEnemyComponents(instance);

		return instance;
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void AddBonusEnemyComponents(GameObject instance)
	{
		if(IsBonus)
		{
			AddTriggerComponent(instance);
			AddColorComponent(instance);
			AddBonusComponent(instance);
		}
	}

	private void AddTriggerComponent(GameObject instance)
	{
		if(instance.TryGetComponent(out RobotTrigger rt))
		{
			Destroy(rt);
		}

		instance.AddComponent<BonusEnemyRobotTrigger>();
	}

	private void AddColorComponent(GameObject instance) => instance.AddComponent<BonusEnemyRobotColor>();
	private void AddBonusComponent(GameObject instance) => instance.AddComponent<BonusEnemyRobotBonus>();

	private void OnTimerEnd()
	{
		telefragger.TelefragGOsWithinRadius();
	}
}