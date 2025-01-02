using UnityEngine;

public class EnemyRobotFreeze : MonoBehaviour
{
	public Timer shootTimer;
	
	public bool Frozen {get; private set;}
	
	private EnemyRobotMovement movement;
	private RobotDisablingManager enemyFreezeManager;
	private Vector2 lastDirection;

	public void SetFreezeState(bool freeze)
	{
		if(freeze)
		{
			SetLastDirection();
		}
		
		Frozen = freeze;
		movement.CurrentMovementDirection = freeze ? Vector2.zero : lastDirection;
		shootTimer.enabled = !freeze;

		movement.SetMovementLock();
	}

	private void Awake()
	{
		movement = GetComponent<EnemyRobotMovement>();
		enemyFreezeManager = FindAnyObjectByType<RobotDisablingManager>(FindObjectsInactive.Include);
	}

	private void Start()
	{
		SetLastDirection();

		if(enemyFreezeManager != null && enemyFreezeManager.EnemiesAreFrozen())
		{
			SetFreezeState(true);
			AdjustAnimation();
		}
	}

	private void AdjustAnimation()
	{
		if(TryGetComponent(out Animator animator))
		{
			animator.SetInteger("MovementY", -1);
		}
	}

	private void SetLastDirection()
	{
		if(!movement.CurrentMovementDirectionIsNone())
		{
			lastDirection = movement.CurrentMovementDirection;
		}
	}
}