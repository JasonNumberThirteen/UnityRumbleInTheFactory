using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : MainMenuData
{
	public int StageNumber {get; private set;}

	public int highScore = 20000;
	public bool twoPlayersMode, isOver, beatenHighScore, enteredStageSelection;
	public Stage[] stages;
	public GameDifficulty difficulty;

	public override int MainMenuCounterValue() => highScore;
	public Stage CurrentStage() => stages[StageNumber - 1];
	public bool StagesDoNotExist() => stages.Length == 0;

	public void ResetData(int initialStage)
	{
		StageNumber = initialStage;
		isOver = beatenHighScore = enteredStageSelection = false;

		difficulty.ResetData();
	}

	public void AdvanceToNextStage()
	{
		StageNumber = StageNumber % stages.Length + 1;
		
		if(StageNumber == 1)
		{
			difficulty.IncreaseDifficulty();
		}
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