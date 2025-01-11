using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityMovementDirectionSelector))]
public class EnemyRobotEntityMovementController : RobotEntityMovementController
{
	[SerializeField] private GameData gameData;
	
	private bool detectedCollision;
	private float lastMovementSpeed;
	private EnemyRobotEntityMovementDirectionSelector enemyRobotEntityMovementDirectionSelector;
	private EnemyRobotEntityMovementControllerTimer enemyRobotEntityMovementControllerTimer;
	private RobotEntitiesDisablingManager robotEntitiesDisablingManager;

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityMovementDirectionSelector = GetComponent<EnemyRobotEntityMovementDirectionSelector>();
		enemyRobotEntityMovementControllerTimer = GetComponentInChildren<EnemyRobotEntityMovementControllerTimer>();
		robotEntitiesDisablingManager = FindAnyObjectByType<RobotEntitiesDisablingManager>(FindObjectsInactive.Include);

		RegisterToListeners(true);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		
		if(DetectedAnyCollision())
		{
			SetDetectedCollisionState(true);
		}
	}

	private void Start()
	{
		SetInitialMovementSpeedModifiedByDifficultyTier();
		SetCurrentMovementDirection(Vector2.down);
	}

	private void SetInitialMovementSpeedModifiedByDifficultyTier()
	{
		if(gameData == null)
		{
			return;
		}
		
		var baseMovementSpeed = movementSpeed;
		var multiplier = gameData.GetDifficultyTierValue(tier => tier.GetEnemyMovementSpeedMultiplier());
		
		SetMovementSpeed(baseMovementSpeed*multiplier);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(enemyRobotEntityMovementControllerTimer != null)
			{
				enemyRobotEntityMovementControllerTimer.onEnd.AddListener(OnTimerEnd);
			}
		}
		else
		{
			if(enemyRobotEntityMovementControllerTimer != null)
			{
				enemyRobotEntityMovementControllerTimer.onEnd.RemoveListener(OnTimerEnd);
			}
		}
	}

	private void OnTimerEnd()
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
		if(robotEntitiesDisablingManager != null && robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled())
		{
			this.lastDirection = lastDirection;
		}
		else
		{
			SetCurrentMovementDirection(currentDirection);
		}
	}

	private void SetCurrentMovementDirection(Vector2 currentDirection)
	{
		CurrentMovementDirection = currentDirection;

		robotEntityCollisionDetector.AdjustRotationIfPossible(CurrentMovementDirection);
	}

	private void SetDetectedCollisionState(bool detected)
	{
		if(detected)
		{
			if(enemyRobotEntityMovementControllerTimer != null)
			{
				enemyRobotEntityMovementControllerTimer.ResetTimer();
			}

			lastMovementSpeed = movementSpeed;
		}

		detectedCollision = detected;

		SetMovementSpeed(detected ? 0f : lastMovementSpeed);
	}

	private bool DetectedAnyCollision()
	{
		var robotEntitiesAreActive = robotEntitiesDisablingManager == null || !robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled();
		var detectedAnyCollision = robotEntityCollisionDetector != null && robotEntityCollisionDetector.OverlapBoxAll().Length > 1;
		
		return !detectedCollision && robotEntitiesAreActive && detectedAnyCollision;
	}
}