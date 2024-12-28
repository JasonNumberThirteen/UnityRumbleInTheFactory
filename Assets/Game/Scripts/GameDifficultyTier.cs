using System;
using UnityEngine;

[Serializable]
public class GameDifficultyTier
{
	[SerializeField, Min(1)] private int enemiesLimitAtOnce;
	[SerializeField, Min(0.01f)] private float enemyMovementSpeedMultiplier;

	public float GetEnemiesLimitAtOnce() => enemiesLimitAtOnce;
	public float GetEnemyMovementSpeedMultiplier() => enemyMovementSpeedMultiplier;
}