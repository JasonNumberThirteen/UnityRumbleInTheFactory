using UnityEngine;

public class EnemyRobotCollider : MonoBehaviour
{
	private EnemyRobotMovement movement;

	private void Awake() => movement = GetComponent<EnemyRobotMovement>();
}