using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityMovementDirectionSelector))]
public class EnemyRobotEntityMovementController : RobotEntityMovementController
{
	[SerializeField] private GameData gameData;
	
	private bool detectedCollision;
	private float lastMovementSpeed;
	private EnemyRobotEntityMovementDirectionSelector enemyRobotEntityMovementDirectionSelector;
	private EnemyRobotEntityMovementControllerTimer enemyRobotEntityMovementControllerTimer;

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityMovementDirectionSelector = GetComponent<EnemyRobotEntityMovementDirectionSelector>();
		enemyRobotEntityMovementControllerTimer = GetComponentInChildren<EnemyRobotEntityMovementControllerTimer>();

		RegisterToListeners(true);
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			if(enemyRobotEntityMovementControllerTimer != null)
			{
				enemyRobotEntityMovementControllerTimer.timerFinishedEvent.AddListener(OnTimerFinished);
			}
		}
		else
		{
			if(enemyRobotEntityMovementControllerTimer != null)
			{
				enemyRobotEntityMovementControllerTimer.timerFinishedEvent.RemoveListener(OnTimerFinished);
			}
		}
	}

	protected override void OnDetectedGameObjectsWereUpdated(List<GameObject> gameObjects)
	{
		if(enabled && !detectedCollision && gameObjects.Count > 0)
		{
			SetDetectedCollisionState(true);
		}
	}

	private void Start()
	{
		SetInitialMovementSpeedModifiedByDifficultyTier();
		SetLastAndCurrentDirection(Vector2.down, Vector2.down);
	}

	private void SetInitialMovementSpeedModifiedByDifficultyTier()
	{
		var multiplier = GameDataMethods.GetDifficultyTierValue(gameData, tier => tier.GetEnemyMovementSpeedMultiplier());
		
		SetMovementSpeed(movementSpeed*multiplier);
	}

	private void OnTimerFinished()
	{
		RandomiseMovementDirection();
		SetDetectedCollisionState(false);
	}

	private void RandomiseMovementDirection()
	{
		var direction = enemyRobotEntityMovementDirectionSelector.GetRandomDirection(CurrentMovementDirection);

		SetLastAndCurrentDirection(direction, direction);
	}

	private void SetLastAndCurrentDirection(Vector2 lastDirection, Vector2 currentDirection)
	{
		this.lastDirection = lastDirection;
		
		if(enabled)
		{
			CurrentMovementDirection = currentDirection;
		}
	}

	private void SetDetectedCollisionState(bool detected)
	{
		if(detected)
		{
			if(enemyRobotEntityMovementControllerTimer != null)
			{
				enemyRobotEntityMovementControllerTimer.StartTimer();
			}

			lastMovementSpeed = movementSpeed;
		}

		detectedCollision = detected;

		SetMovementSpeed(detected ? 0f : lastMovementSpeed);
	}
}