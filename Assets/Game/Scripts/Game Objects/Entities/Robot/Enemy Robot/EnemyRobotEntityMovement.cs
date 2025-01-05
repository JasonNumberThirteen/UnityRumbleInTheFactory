using UnityEngine;

public class EnemyRobotEntityMovement : RobotEntityMovement
{
	private bool detectedCollision;
	private float lastMovementSpeed;
	private EnemyRobotEntityMovementDirectionSelector enemyRobotEntityMovementDirectionSelector;
	private EnemyRobotEntityMovementTimer enemyRobotEntityMovementTimer;
	private RobotEntitiesDisablingManager robotEntitiesDisablingManager;

	public void SetMovementLock()
	{
		if(LastDirectionIsNotZero())
		{
			SetDirections(CurrentMovementDirection, lastDirection);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityMovementDirectionSelector = GetComponent<EnemyRobotEntityMovementDirectionSelector>();
		enemyRobotEntityMovementTimer = GetComponentInChildren<EnemyRobotEntityMovementTimer>();
		robotEntitiesDisablingManager = FindAnyObjectByType<RobotEntitiesDisablingManager>(FindObjectsInactive.Include);

		RegisterToListeners(true);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		DetectObstacles();
	}

	private void Start()
	{
		SetMovementSpeed(GetMovementSpeed()*StageManager.instance.gameData.GetDifficultyTierValue(tier => tier.GetEnemyMovementSpeedMultiplier()));
		SetDirection(Vector2.down);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(enemyRobotEntityMovementTimer != null)
			{
				enemyRobotEntityMovementTimer.onEnd.AddListener(OnTimerEnd);
			}
		}
		else
		{
			if(enemyRobotEntityMovementTimer != null)
			{
				enemyRobotEntityMovementTimer.onEnd.RemoveListener(OnTimerEnd);
			}
		}
	}

	private void OnTimerEnd()
	{
		RandomiseDirection();
		SetCollisionDetectionState(false);
	}

	private void RandomiseDirection()
	{
		Vector2 direction = enemyRobotEntityMovementDirectionSelector.GetRandomDirection(CurrentMovementDirection);

		SetDirections(direction, direction);
	}

	private void SetDirections(Vector2 newLastDirection, Vector2 newDirection)
	{
		if(robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled())
		{
			lastDirection = newLastDirection;
		}
		else
		{
			SetDirection(newDirection);
		}
	}

	private void SetDirection(Vector2 direction)
	{
		CurrentMovementDirection = direction;

		robotEntityRotation.RotateByDirection(CurrentMovementDirection);
	}

	private void SetCollisionDetectionState(bool detected)
	{
		if(detected)
		{
			if(enemyRobotEntityMovementTimer != null)
			{
				enemyRobotEntityMovementTimer.ResetTimer();
			}

			lastMovementSpeed = GetMovementSpeed();
		}

		detectedCollision = detected;

		SetMovementSpeed(detected ? 0f : lastMovementSpeed);
	}

	private void DetectObstacles()
	{
		if(DetectedCollision())
		{
			SetCollisionDetectionState(true);
		}
	}

	private bool LastDirectionIsNotZero() => lastDirection != Vector2.zero;
	private bool DetectedCollision() => !detectedCollision && !robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled() && robotEntityCollisionDetector.OverlapBoxAll().Length > 1;
}