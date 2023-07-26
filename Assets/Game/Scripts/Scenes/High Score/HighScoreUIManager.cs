using TMPro;
using UnityEngine;

public class HighScoreUIManager : MonoBehaviour
{
	public GameData gameData;
	public TextMeshProUGUI highScoreText;

	private void Start() => highScoreText.text = "HISCORE\n" + FormattedScore(gameData.highScore);
	private string FormattedScore(int score) => string.Format("{0,7}", score);
}