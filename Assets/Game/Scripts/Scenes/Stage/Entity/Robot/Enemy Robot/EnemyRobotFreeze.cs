using UnityEngine;

public class EnemyRobotFreeze : MonoBehaviour
{
	public Timer shootTimer;
	
	public bool Frozen {get; private set;}
	
	private EnemyRobotMovement movement;
	private Vector2 lastDirection;

	public void Freeze()
	{
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
	
	private void Awake() => movement = GetComponent<EnemyRobotMovement>();

	private void Start()
	{
		SetLastDirection(movement.Direction);

		if(StageManager.instance.EnemiesAreFrozen())
		{
			SetState(true);
			GetComponent<Animator>().SetInteger("MovementY", -1);
		}
	}

	private void SetState(bool freeze)
	{
		Frozen = freeze;
		movement.Direction = (Frozen) ? Vector2.zero : lastDirection;
		shootTimer.enabled = !Frozen;

		if(!freeze)
		{
			movement.Unfreeze();
		}
		else
		{
			movement.Freeze();
		}
	}
}