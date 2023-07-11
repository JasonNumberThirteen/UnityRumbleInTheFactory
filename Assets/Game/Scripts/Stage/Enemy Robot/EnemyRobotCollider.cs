using UnityEngine;

public class EnemyRobotCollider : MonoBehaviour
{
	private EnemyRobotMovement movement;

	private void Awake() => movement = GetComponent<EnemyRobotMovement>();
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		movement.RandomiseDirection();
	}
}