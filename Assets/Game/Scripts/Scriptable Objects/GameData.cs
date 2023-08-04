using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : ScriptableObject
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
	public bool twoPlayersMode, isOver, beatenHighScore;
	public Stage[] stages;

	private int stageNumber = 1;

	public void ResetData() => isOver = beatenHighScore = false;
}