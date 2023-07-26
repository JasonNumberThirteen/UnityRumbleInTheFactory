using TMPro;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	public PlayerData playerData;
	public GameData gameData;
	public TextMeshProUGUI playerOneScoreCounter, highScoreCounter;

	private string FormattedScore(int score) => string.Format("{0,6}", score);

	private void Start()
	{
		playerOneScoreCounter.text = "I-" + FormattedScore(playerData.Score);
		highScoreCounter.text = "HI-" + FormattedScore(gameData.highScore);
	}
}