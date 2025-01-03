using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : ScriptableObject
{
	public bool EnteredStageSelection {get; private set;}
	public bool SelectedTwoPlayersMode {get; private set;}
	public bool GameIsOver {get; private set;}
	public bool BeatenHighScore {get; private set;}
	public int HighScore {get; private set;} = 20000;
	public int StageNumber {get; private set;}
	public StageData[] StagesData {get; private set;}

	[SerializeField] private GameDifficulty gameDifficulty;
	
	public int GetCurrentDifficultyTierIndex() => gameDifficulty.GetCurrentTierIndex();
	public StageData GetCurrentStageData() => StagesData != null && StageNumber >= 1 && StageNumber <= StagesData.Length ? StagesData[StageNumber - 1] : null;
	public bool NoStagesFound() => StagesData.Length == 0;

	public void ResetData(int initialStage)
	{
		StageNumber = initialStage;
		GameIsOver = BeatenHighScore = EnteredStageSelection = false;

		gameDifficulty.ResetData();
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
		if(HighScore > score)
		{
			return;
		}
		
		HighScore = score;

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

	public T GetDifficultyTierValue<T>(Func<GameDifficultyTier, T> tierFunc) where T : struct
	{
		return gameDifficulty.GetTierValue(tierFunc);
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
		StageNumber = StageNumber % StagesData.Length + 1;
	}
}