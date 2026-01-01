using System;

[Serializable]
public class SaveableGameData
{
	public int highScore;
	public int stageNumber;

	private static readonly int INITIAL_HIGH_SCORE = 20000;

	public void ResetData()
	{
		highScore = INITIAL_HIGH_SCORE;
		stageNumber = 0;
	}
}