using UnityEngine;

[RequireComponent(typeof(Telefragger), typeof(IntersectingGameObjectsDetector))]
public class EnemyRobotEntitySpawner : EntitySpawner
{
	public bool IsBonus {get; set;}

	[SerializeField, Min(1)] private int ordinalNumber;
	[SerializeField] private Color bonusEnemyColor = Color.blue;
	[SerializeField] private float bonusEnemyColorFadeTime = 0.5f;

	private Telefragger telefragger;
	private IntersectingGameObjectsDetector intersectingGameObjectsDetector;

	public int GetOrdinalNumber() => ordinalNumber;

	protected override void Awake()
	{
		base.Awake();

		telefragger = GetComponent<Telefragger>();
		intersectingGameObjectsDetector = GetComponent<IntersectingGameObjectsDetector>();
	}

	protected override GameObject GetEntityInstance()
	{
		var entityInstance = base.GetEntityInstance();

		if(IsBonus && entityInstance != null && entityInstance.TryGetComponent(out EnemyRobotEntity enemyRobotEntity))
		{
			enemyRobotEntity.SetupForBonusType(bonusEnemyColor, bonusEnemyColorFadeTime);
		}

		return entityInstance;
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerFinished()
	{
		telefragger.TelefragGOsWithinRadius();
		intersectingGameObjectsDetector.SetLayerToGOsIfIntersect();
	}
}