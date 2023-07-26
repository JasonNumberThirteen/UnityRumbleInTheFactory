using TMPro;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
	public PlayerData playerOneData, playerTwoData;
	public GameData gameData;
	public TextMeshProUGUI playerOneScoreCounter, playerTwoScoreCounter, highScoreCounter;

	private string FormattedScore(int score) => string.Format("{0,6}", score);

	private void Start()
	{
		playerOneScoreCounter.text = "I-" + FormattedScore(playerOneData.Score);
		playerTwoScoreCounter.text = gameData.twoPlayersMode ? "II-" + FormattedScore(playerTwoData.Score) : string.Empty;
		highScoreCounter.text = "HI-" + FormattedScore(gameData.highScore);
	}
}