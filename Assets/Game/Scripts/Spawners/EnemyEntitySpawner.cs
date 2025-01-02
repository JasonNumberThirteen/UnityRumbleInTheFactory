using System;
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
		var entityInstance = base.GetEntityInstance();

		if(entityInstance != null)
		{
			AddBonusEnemyComponentsToEntityIfNeeded(entityInstance);
		}

		return entityInstance;
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

	private void AddBonusEnemyComponentsToEntityIfNeeded(GameObject entity)
	{
		if(!IsBonus || entity == null)
		{
			return;
		}

		AddComponentToEntityIfPossible<BonusEnemyRobotTrigger>(entity, () =>
		{
			if(entity != null && entity.TryGetComponent(out RobotTriggerEventsSender robotTrigger))
			{
				Destroy(robotTrigger);
			}
		});
		AddComponentToEntityIfPossible<BonusEnemyRobotColor>(entity);
	}

	private void AddComponentToEntityIfPossible<T>(GameObject entity, Action onStart = null) where T : Component
	{
		onStart?.Invoke();

		if(entity != null)
		{
			entity.AddComponent<T>();
		}
	}

	private void OnTimerEnd()
	{
		telefragger.TelefragGOsWithinRadius();
	}
}