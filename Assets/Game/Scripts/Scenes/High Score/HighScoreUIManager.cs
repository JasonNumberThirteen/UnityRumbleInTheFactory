using UnityEngine;

public class HighScoreUIManager : MonoBehaviour
{
	public GameData gameData;
	public Counter counter;

	private void Awake() => counter.SetTo(gameData.highScore);
}