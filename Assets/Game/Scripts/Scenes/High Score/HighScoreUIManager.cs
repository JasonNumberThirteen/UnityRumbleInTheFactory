using UnityEngine;

public class HighScoreUIManager : MonoBehaviour
{
	public GameData gameData;
	public IntCounter counter;

	private void Start() => counter.SetTo(gameData.highScore);
}