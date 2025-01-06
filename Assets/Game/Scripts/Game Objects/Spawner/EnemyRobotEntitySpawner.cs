using System;
using UnityEngine;

[RequireComponent(typeof(Telefragger))]
public class EnemyRobotEntitySpawner : EntitySpawner
{
	public bool IsBonus {get; set;}

	[SerializeField, Min(1)] private int ordinalNumber;

	private Telefragger telefragger;

	public int GetOrdinalNumber() => ordinalNumber;

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
			AddBonusEnemyComponentsToEntityGOIfNeeded(entityInstance);
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

	private void AddBonusEnemyComponentsToEntityGOIfNeeded(GameObject entityGO)
	{
		if(!IsBonus || entityGO == null)
		{
			return;
		}

		AddComponentToEntityGOIfPossible<BonusEnemyRobotEntityTriggerEventsReceiver>(entityGO, () =>
		{
			if(entityGO != null && entityGO.TryGetComponent(out RobotEntityTriggerEventsReceiver robotEntityTriggerEventsReceiver))
			{
				Destroy(robotEntityTriggerEventsReceiver);
			}
		});
		AddComponentToEntityGOIfPossible<BonusEnemyRobotColor>(entityGO);
	}

	private void AddComponentToEntityGOIfPossible<T>(GameObject entityGO, Action onStart = null) where T : Component
	{
		onStart?.Invoke();

		if(entityGO != null)
		{
			entityGO.AddComponent<T>();
		}
	}

	private void OnTimerEnd()
	{
		telefragger.TelefragGOsWithinRadius();
	}
}