using TMPro;
using System.Linq;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public ScorePointsRowsBuilder pointsRowsBuilder;
	public PlayerData playerData;
	public GameData gameData;
	public RectTransformPositionController totalTextMover, horizontalLineMover, totalDefeatedEnemiesCounterMover;
	public Timer enemyTypeSwitch, scoreCountTimer, sceneManagerTimer;
	public TextMeshProUGUI highScoreCounter, playerOneScoreCounter, totalDefeatedEnemiesCounter;
	public AudioSource audioSource;

	private int enemyTypeIndex, countedEnemies, totalCountedEnemies, enemyTypeScore, defeatedEnemies, scorePerEnemy;
	private TextMeshProUGUI currentDefeatedEnemiesCounter, currentEnemyTypePointsCounter;
	private EnemyData[] defeatedEnemiesData;
	private int[] defeatedEnemiesCount;

	public void GoToNextEnemyType()
	{
		if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			currentDefeatedEnemiesCounter = pointsRowsBuilder.DefeatedEnemiesCounters[enemyTypeIndex];
			currentEnemyTypePointsCounter = pointsRowsBuilder.EnemyTypePointsCounters[enemyTypeIndex];
			defeatedEnemies = defeatedEnemiesCount[enemyTypeIndex];
			scorePerEnemy = defeatedEnemiesData[enemyTypeIndex].GetPointsForDefeat();
			++enemyTypeIndex;
			countedEnemies = enemyTypeScore = 0;
		}
		else if(TotalDefeatedEnemiesCounterIsNotAssignedYet())
		{
			SetTotalDefeatedEnemiesCounterText();
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
		RetrieveEnemiesData();
		RetrieveEnemiesCount();
		pointsRowsBuilder.SetDefeatedEnemiesSprites(defeatedEnemiesData.Select(e => e.GetDisplayInScoreSceneSprite()).ToArray());
		pointsRowsBuilder.BuildPointsRows();
		SetLastElementsPosition();
	}

	private void ResetTotalDefeatedEnemiesCounter() => totalDefeatedEnemiesCounter.text = string.Empty;
	private void SetHighScore() => highScoreCounter.text = gameData.HighScore.ToString();
	private void SetPlayerOneScore() => playerOneScoreCounter.text = playerData.Score.ToString();
	public void RetrieveEnemiesData() => defeatedEnemiesData = playerData.DefeatedEnemies.Keys.ToArray();
	private void RetrieveEnemiesCount() => defeatedEnemiesCount = playerData.DefeatedEnemies.Values.ToArray();
	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;
	private bool TotalDefeatedEnemiesCounterIsNotAssignedYet() => totalDefeatedEnemiesCounter.text != TotalDefeatedEnemiesCounterText();
	private void SetTotalDefeatedEnemiesCounterText() => totalDefeatedEnemiesCounter.text = TotalDefeatedEnemiesCounterText();
	private string TotalDefeatedEnemiesCounterText() => totalCountedEnemies.ToString();

	private void SetLastElementsPosition()
	{
		int offsetY = -16*DefeatedEnemiesTypes();
		
		totalTextMover.AddPositionY(offsetY);
		horizontalLineMover.AddPositionY(offsetY);
		totalDefeatedEnemiesCounterMover.SetPositionY(totalTextMover.GetPositionY());
	}
}