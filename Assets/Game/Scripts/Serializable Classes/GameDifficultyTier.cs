using System;
using UnityEngine;

[Serializable]
public class GameDifficultyTier
{
	[SerializeField, Min(1f)] private float enemySpawnDelay;
	[SerializeField, Min(1)] private int enemiesLimitAtOnce;
	[SerializeField, Min(0.01f)] private float enemyMovementSpeedMultiplier;
	[SerializeField, Min(0.01f)] private float enemyShootDelayMultiplier;
	[SerializeField] private bool enemiesCanCollectBonuses;

	public float GetEnemySpawnDelay() => enemySpawnDelay;
	public int GetEnemiesLimitAtOnce() => enemiesLimitAtOnce;
	public float GetEnemyMovementSpeedMultiplier() => enemyMovementSpeedMultiplier;
	public float GetEnemyShootDelayMultiplier() => enemyShootDelayMultiplier;
	public bool EnemiesCanCollectBonuses() => enemiesCanCollectBonuses;
}