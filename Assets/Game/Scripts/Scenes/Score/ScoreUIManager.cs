using TMPro;
using System.Linq;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public ScorePointsRowsBuilder pointsRowsBuilder;
	public PlayerData playerData;
	public GameData gameData;
	public RectTransform totalText, horizontalLine;
	public Timer enemyTypeSwitch, scoreCountTimer, sceneManagerTimer;
	public TextMeshProUGUI highScoreCounter, playerOneScoreCounter, totalDefeatedEnemiesCounter;
	public AudioSource audioSource;

	private int enemyTypeIndex, countedEnemies, totalCountedEnemies, enemyTypeScore, defeatedEnemies, scorePerEnemy;
	private TextMeshProUGUI currentDefeatedEnemiesCounter, currentEnemyTypePointsCounter;
	private int[] defeatedEnemiesCount;

	public void GoToNextEnemyType()
	{
		if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			currentDefeatedEnemiesCounter = pointsRowsBuilder.DefeatedEnemiesCounters[enemyTypeIndex];
			currentEnemyTypePointsCounter = pointsRowsBuilder.EnemyTypePointsCounters[enemyTypeIndex];
			defeatedEnemies = defeatedEnemiesCount[enemyTypeIndex];
			scorePerEnemy = pointsRowsBuilder.DefeatedEnemiesData[enemyTypeIndex].score;
			++enemyTypeIndex;
			countedEnemies = enemyTypeScore = 0;
		}
		else if(totalDefeatedEnemiesCounter.text != totalCountedEnemies.ToString())
		{
			totalDefeatedEnemiesCounter.text = totalCountedEnemies.ToString();

			sceneManagerTimer.StartTimer();
		}
	}

	public void CountPoints()
	{
		if(countedEnemies < defeatedEnemies)
		{
			++countedEnemies;
			++totalCountedEnemies;
			enemyTypeScore += scorePerEnemy;
			currentDefeatedEnemiesCounter.text = countedEnemies.ToString();
			currentEnemyTypePointsCounter.text = enemyTypeScore.ToString();

			scoreCountTimer.ResetTimer();
			audioSource.Play();
		}
		else
		{
			enemyTypeSwitch.ResetTimer();
		}
	}

	private void Start()
	{
		ResetTotalDefeatedEnemiesCounter();
		SetHighScore();
		SetPlayerOneScore();
		pointsRowsBuilder.RetrieveEnemiesData();
		RetrieveEnemiesCount();
		pointsRowsBuilder.BuildPointsRows();
		SetTotalTextPosition();
	}

	private void ResetTotalDefeatedEnemiesCounter() => totalDefeatedEnemiesCounter.text = string.Empty;
	private void SetHighScore() => highScoreCounter.text = gameData.highScore.ToString();
	private void SetPlayerOneScore() => playerOneScoreCounter.text = playerData.Score.ToString();
	private void RetrieveEnemiesCount() => defeatedEnemiesCount = playerData.DefeatedEnemies.Values.ToArray();
	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;

	private void SetTotalTextPosition()
	{
		int offsetY = -16*DefeatedEnemiesTypes();
		
		totalText.anchoredPosition = new Vector2(totalText.anchoredPosition.x, totalText.anchoredPosition.y + offsetY);
		horizontalLine.anchoredPosition = new Vector2(horizontalLine.anchoredPosition.x, horizontalLine.anchoredPosition.y + offsetY);

		if(totalDefeatedEnemiesCounter.TryGetComponent(out RectTransform rt))
		{
			rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, totalText.anchoredPosition.y);
		}
	}
}