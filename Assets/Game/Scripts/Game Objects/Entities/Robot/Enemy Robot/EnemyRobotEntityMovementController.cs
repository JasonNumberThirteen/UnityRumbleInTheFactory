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

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
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
		if(!enabled)
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
		var detectedColliders = robotEntityCollisionDetector != null ? robotEntityCollisionDetector.OverlapBoxAll() : new Collider2D[0];
		
		return !detectedCollision && enabled && detectedColliders.Length > 0;
	}
}