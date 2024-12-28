using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : MainMenuData
{
	public int StageNumber {get; private set;}

	public int highScore = 20000;
	public bool twoPlayersMode, isOver, beatenHighScore, enteredStageSelection;
	public StageData[] stages;
	public GameDifficulty difficulty;

	public override int MainMenuCounterValue() => highScore;
	public StageData CurrentStage() => stages[StageNumber - 1];
	public bool StagesDoNotExist() => stages.Length == 0;

	public void ResetData(int initialStage)
	{
		StageNumber = initialStage;
		isOver = beatenHighScore = enteredStageSelection = false;

		difficulty.ResetData();
	}

	public void IncreaseDifficultyIfNeeded()
	{
		if(StageNumber == stages.Length)
		{
			difficulty.IncreaseDifficulty();
		}
	}

	public void AdvanceToNextStage()
	{
		StageNumber = StageNumber % stages.Length + 1;
	}
}