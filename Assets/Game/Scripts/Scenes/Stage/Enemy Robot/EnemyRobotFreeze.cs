using UnityEngine;
using System.Collections;

public class EnemyRobotFreeze : MonoBehaviour
{
	public bool Frozen {get; private set;}
	
	private EnemyRobotMovement movement;
	private EnemyRobotShoot shoot;

	public void Freeze(float duration)
	{
		StartCoroutine(FreezeYourself(duration));
	}

	private void Awake()
	{
		movement = GetComponent<EnemyRobotMovement>();
		shoot = GetComponent<EnemyRobotShoot>();
	}

	private IEnumerator FreezeYourself(float duration)
	{
		Vector2 movementDirection = movement.Direction;
		
		Frozen = true;
		movement.Direction = Vector2.zero;

		yield return new WaitForSeconds(duration);

		Frozen = false;
		movement.Direction = movementDirection;
	}
}