using UnityEngine;

public class EnemyRobotFreeze : MonoBehaviour
{
	public bool Frozen {get; private set;}
	
	private EnemyRobotMovement movement;
	private Vector2 lastDirection;
	private Timer shootTimer;

	public void Freeze()
	{
		//lastDirection = movement.Direction;
		SetLastDirection(movement.Direction);
		SetState(true);
	}

	public void Unfreeze() => SetState(false);

	private void SetLastDirection(Vector2 direction)
	{
		if(direction != Vector2.zero)
		{
			lastDirection = movement.Direction;
		}
	}
	
	private void Awake()
	{
		movement = GetComponent<EnemyRobotMovement>();
		shootTimer = GetComponent<Timer>();
	}

	private void Start()
	{
		//lastDirection = movement.Direction;
		SetLastDirection(movement.Direction);

		if(StageManager.instance.EnemiesAreFrozen())
		{
			SetState(true);
		}
	}

	private void SetState(bool freeze)
	{
		Frozen = freeze;
		movement.Direction = (Frozen) ? Vector2.zero : lastDirection;
		shootTimer.enabled = !Frozen;
	}
}