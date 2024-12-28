using System;
using UnityEngine;

[Serializable]
public class GameDifficulty
{
	[SerializeField] [Min(0)] private int tier;
	[SerializeField] private GameDifficultyTier[] tiers;

	public void ResetData() => tier = 0;
	public float EnemiesMovementSpeedMultiplier() => tiers[tier].GetEnemyMovementSpeedMultiplier();
	public int EnemiesLimit() => tiers[tier].GetEnemiesLimitAtOnce();
	public int CurrentTier() => tier;

	public void IncreaseDifficulty() => tier = Mathf.Clamp(tier + 1, 0, tiers.Length - 1);
}