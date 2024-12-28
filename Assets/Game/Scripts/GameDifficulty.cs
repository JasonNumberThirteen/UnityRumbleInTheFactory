using System;
using UnityEngine;

[Serializable]
public class GameDifficulty
{
	[SerializeField] [Min(0)] private int tier;
	[SerializeField] [Min(0)] private int maxTier = 5;

	[SerializeField] private float[] enemiesMovementSpeedMultiplierPerTier;
	[SerializeField] private int[] enemiesLimitPerTier;

	public void ResetData() => tier = 0;
	public float EnemiesMovementSpeedMultiplier() => enemiesMovementSpeedMultiplierPerTier[tier];
	public int EnemiesLimit() => enemiesLimitPerTier[tier];
	public int CurrentTier() => tier;

	public void IncreaseDifficulty() => tier = Mathf.Clamp(tier + 1, 0, maxTier);
}