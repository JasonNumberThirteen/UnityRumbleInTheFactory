using UnityEngine;

public class EnemyRobotEntityMovement : RobotEntityMovement
{
	public Timer timer;

	private bool detectedCollision;
	private float lastMovementSpeed;
	private EnemyRobotEntityMovementDirectionSelector enemyRobotEntityMovementDirectionSelector;
	private RobotDisablingManager robotDisablingManager;

	public void SetMovementLock()
	{
		if(LastDirectionIsNotZero())
		{
			SetDirections(CurrentMovementDirection, lastDirection);
		}
	}

	public void RandomiseDirection()
	{
		Vector2 direction = enemyRobotEntityMovementDirectionSelector.GetRandomDirection(CurrentMovementDirection);

		SetDirections(direction, direction);
	}

	public void SetCollisionDetectionState(bool detected)
	{
		if(detected)
		{
			timer.ResetTimer();

			lastMovementSpeed = GetMovementSpeed();
		}

		detectedCollision = detected;

		SetMovementSpeed(detected ? 0f : lastMovementSpeed);
	}

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityMovementDirectionSelector = GetComponent<EnemyRobotEntityMovementDirectionSelector>();
		robotDisablingManager = FindAnyObjectByType<RobotDisablingManager>(FindObjectsInactive.Include);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		DetectObstacles();
	}

	private void SetDirections(Vector2 newLastDirection, Vector2 newDirection)
	{
		if(robotDisablingManager.RobotsAreTemporarilyDisabled())
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

		robotRotation.RotateByDirection(CurrentMovementDirection);
	}

	private void Start()
	{
		SetMovementSpeed(GetMovementSpeed()*StageManager.instance.gameData.GetDifficultyTierValue(tier => tier.GetEnemyMovementSpeedMultiplier()));
		SetDirection(Vector2.down);
	}

	private bool LastDirectionIsNotZero() => lastDirection != Vector2.zero;
	private bool DetectedCollision() => !detectedCollision && !robotDisablingManager.RobotsAreTemporarilyDisabled() && robotEntityCollisionDetector.OverlapBoxAll().Length > 1;
	
	private void DetectObstacles()
	{
		if(DetectedCollision())
		{
			SetCollisionDetectionState(true);
		}
	}
}