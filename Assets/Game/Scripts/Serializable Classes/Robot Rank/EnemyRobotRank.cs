using System;
using UnityEngine;

[Serializable]
public class EnemyRobotRank : RobotRank
{
	[SerializeField] private EnemyRobotData enemyRobotData;
	[SerializeField, Min(0f)] private float shootDelay = 1f;

	public EnemyRobotData GetEnemyRobotData() => enemyRobotData;
	public float GetShootDelay() => shootDelay;
}