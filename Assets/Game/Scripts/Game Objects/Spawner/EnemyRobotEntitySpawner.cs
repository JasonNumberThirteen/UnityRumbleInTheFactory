using System;
using UnityEngine;

[RequireComponent(typeof(Telefragger))]
public class EnemyRobotEntitySpawner : EntitySpawner
{
	public bool IsBonus {get; set;}

	[SerializeField, Min(1)] private int ordinalNumber;
	[SerializeField] private float bonusEnemyColorFadeTime = 0.5f;
	[SerializeField] private Color bonusEnemyColor = Color.blue;

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
		AddComponentToEntityGOIfPossible<BonusEnemyRobotEntityRendererColorAdjuster>(entityGO, actionOnComponent: component => component.Setup(bonusEnemyColorFadeTime, bonusEnemyColor));
	}

	private void AddComponentToEntityGOIfPossible<T>(GameObject entityGO, Action onStart = null, Action<T> actionOnComponent = null) where T : Component
	{
		onStart?.Invoke();

		if(entityGO != null)
		{
			var component = entityGO.AddComponent<T>();

			actionOnComponent?.Invoke(component);
		}
	}

	private void OnTimerEnd()
	{
		telefragger.TelefragGOsWithinRadius();
	}
}