using System;
using UnityEngine;

[Serializable]
public class EnemyRobotRank : RobotRank
{
	[SerializeField] private EnemyRobotData enemyRobotData;

	public EnemyRobotData GetEnemyRobotData() => enemyRobotData;
}