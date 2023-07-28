using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : ScriptableObject
{
	public int stageNumber = 1, highScore = 20000;
	public bool twoPlayersMode, isOver, beatenHighScore;
	public Stage[] stages;

	public void ResetData() => isOver = beatenHighScore = false;
}