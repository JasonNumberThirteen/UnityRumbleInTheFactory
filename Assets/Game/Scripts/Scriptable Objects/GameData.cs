using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : ScriptableObject
{
	public SaveableGameData saveableGameData = new();

	public int HighScore
	{
		get => saveableGameData.highScore;
	}
	public int StageNumber
	{
		get => saveableGameData.stageNumber;
	}
	public bool EnteredStageSelection {get; private set;}
	public bool SelectedTwoPlayersMode {get; private set;}
	public bool GameIsOver {get; private set;}
	public bool BeatenHighScore {get; private set;}
	public int PreviousHighScore {get; private set;}
	public StageData[] StagesData {get; private set;}

	[SerializeField] private GameDifficulty gameDifficulty = new();
	
	public int GetCurrentDifficultyTierIndex() => gameDifficulty.GetCurrentTierIndex();
	public T GetDifficultyTierValue<T>(Func<GameDifficultyTier, T> tierFunc) where T : struct => gameDifficulty.GetTierValue(tierFunc);
	public StageData GetCurrentStageData() => StagesData != null && StageNumber >= 1 && StageNumber <= StagesData.Length ? StagesData[StageNumber - 1] : null;
	public bool NoStagesFound() => StagesData.Length == 0;

	public void ResetData()
	{
		GameIsOver = BeatenHighScore = EnteredStageSelection = false;
		PreviousHighScore = HighScore;

		gameDifficulty.ResetData();
	}

	public void SetStageNumber(int stageNumber)
	{
		saveableGameData.stageNumber = stageNumber;
	}

	public void SetupForGameStart(bool selectedTwoPlayersMode)
	{
		EnteredStageSelection = true;
		SelectedTwoPlayersMode = selectedTwoPlayersMode;
	}

	public void SetGameAsOver()
	{
		GameIsOver = true;
	}

	public void SetHighScoreIfPossible(int score, Action onBeatenHighScore = null)
	{
		if(HighScore >= score)
		{
			return;
		}
		
		saveableGameData.highScore = score;

		if(!BeatenHighScore)
		{
			BeatenHighScore = true;

			onBeatenHighScore?.Invoke();
		}
	}

	public void SetStagesData(StageData[] stagesData)
	{
		StagesData = stagesData;
	}

	public void IncreaseDifficultyIfNeeded()
	{
		if(StageNumber == StagesData.Length)
		{
			gameDifficulty.IncreaseDifficulty();
		}
	}

	public void AdvanceToNextStage()
	{
		if(StagesData != null && StagesData.Length > 0)
		{
			saveableGameData.stageNumber = StageNumber % StagesData.Length + 1;
		}
	}
}