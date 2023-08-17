using UnityEngine;

public class HighScoreUIManager : MonoBehaviour
{
	public GameData gameData;
	public Counter counter;

	private void Start() => counter.SetTo(gameData.highScore);
}