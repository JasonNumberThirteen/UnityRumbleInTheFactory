using System;
using UnityEngine;

[Serializable]
public class GameDifficulty
{
	[SerializeField] private GameDifficultyTier[] tiers;

	private int currentTierIndex;

	public void ResetData()
	{
		currentTierIndex = 0;
	}

	public int GetCurrentTierIndex() => currentTierIndex;

	public void IncreaseDifficulty()
	{
		if(tiers.Length > 0)
		{
			currentTierIndex = Mathf.Clamp(currentTierIndex + 1, 0, tiers.Length - 1);
		}
	}

	public T GetTierValue<T>(Func<GameDifficultyTier, T> tierFunc) where T : struct
	{
		var currentTier = GetCurrentTier();
		
		return currentTier != null ? tierFunc(currentTier) : default;
	}

	private GameDifficultyTier GetCurrentTier() => tiers.Length > 0 && currentTierIndex >= 0 && currentTierIndex < tiers.Length ? tiers[currentTierIndex] : null;
}