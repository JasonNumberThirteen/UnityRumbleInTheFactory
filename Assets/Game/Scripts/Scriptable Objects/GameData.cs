using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : MainMenuData
{
	public int StageNumber
	{
		get
		{
			return stageNumber;
		}
		set
		{
			stageNumber = (value - 1) % stages.Length + 1;
		}
	}
	
	public int highScore = 20000;
	public bool twoPlayersMode, isOver, beatenHighScore, enteredStageSelection;
	public Stage[] stages;
	public GameDifficulty difficulty;

	private int stageNumber = 1;

	public override int MainMenuCounterValue() => highScore;
	public Stage CurrentStage() => stages[stageNumber - 1];

	public void ResetData()
	{
		isOver = beatenHighScore = enteredStageSelection = false;

		difficulty.ResetData();
	}
}

[System.Serializable]
public class GameDifficulty
{
	[SerializeField] [Min(0)] private int tier;
	[SerializeField] [Min(0)] private int maxTier = 5;

	[SerializeField] private float[] enemiesMovementSpeedMultiplierPerTier;
	[SerializeField] private int[] enemiesLimitPerTier;

	public void ResetData() => tier = 0;
	public float EnemiesMovementSpeedMultiplier() => enemiesMovementSpeedMultiplierPerTier[tier];
	public int EnemiesLimit() => enemiesLimitPerTier[tier];

	public void IncreaseDifficulty() => tier = Mathf.Clamp(tier + 1, 0, maxTier);
}