using UnityEngine;

public class EnemyRobotFreeze : MonoBehaviour
{
	public bool Frozen {get; private set;}
	
	private EnemyRobotMovement movement;
	private Vector2 lastDirection;

	public void Freeze()
	{
		lastDirection = movement.Direction;
		
		SetState(true);
	}

	public void Unfreeze() => SetState(false);
	
	private void Awake() => movement = GetComponent<EnemyRobotMovement>();

	private void Start()
	{
		lastDirection = movement.Direction;

		if(StageManager.instance.EnemiesAreFrozen())
		{
			SetState(true);
		}
	}

	private void SetState(bool freeze)
	{
		Frozen = freeze;
		movement.Direction = (Frozen) ? Vector2.zero : lastDirection;
	}
}