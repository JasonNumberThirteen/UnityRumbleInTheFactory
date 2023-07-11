using UnityEngine;

public class EnemyRobotCollider : MonoBehaviour
{
	private EnemyRobotMovement movement;

	private void Awake() => movement = GetComponent<EnemyRobotMovement>();

	private void OnCollisionEnter2D(Collision2D collider)
	{
		if(collider.gameObject.CompareTag(gameObject.tag))
		{
			movement.RandomiseDirection();
		}
	}
}