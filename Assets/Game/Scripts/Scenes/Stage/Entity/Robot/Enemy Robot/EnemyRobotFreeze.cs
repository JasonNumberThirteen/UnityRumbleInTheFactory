using UnityEngine;
using System.Collections;

public class EnemyRobotFreeze : MonoBehaviour
{
	public bool Frozen {get; private set;}
	
	private EnemyRobotMovement movement;
	private Vector2 lastDirection;

	public void Freeze(float duration) => StartCoroutine(FreezeYourself(duration));

	private void Awake() => movement = GetComponent<EnemyRobotMovement>();
	
	private IEnumerator FreezeYourself(float duration)
	{
		lastDirection = movement.Direction;
		
		SetState(true);

		yield return new WaitForSeconds(duration);

		SetState(false);
	}

	private void SetState(bool freeze)
	{
		Frozen = freeze;
		movement.Direction = (Frozen) ? Vector2.zero : lastDirection;
	}
}