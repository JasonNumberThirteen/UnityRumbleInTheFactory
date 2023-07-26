using TMPro;
using System.Linq;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public PlayerData playerData;
	public GameData gameData;
	public RectTransform parent; 
	public GameObject pointsText, defeatedEnemiesCounter, leftArrow, enemyType, enemyTypePointsCounter;
	public RectTransform totalText, horizontalLine;
	public Timer enemyTypeSwitch, scoreCountTimer, sceneManagerTimer;
	public TextMeshProUGUI highScoreCounter, playerOneScoreCounter, totalDefeatedEnemiesCounter;

	private int enemyTypeIndex, countedEnemies, totalCountedEnemies, enemyTypeScore, defeatedEnemies, scorePerEnemy;
	private TextMeshProUGUI[] defeatedEnemiesCounters, enemyTypePointsCounters;
	private TextMeshProUGUI currentDefeatedEnemiesCounter, currentEnemyTypePointsCounter;
	private EnemyData[] defeatedEnemiesData;
	private int[] defeatedEnemiesCount;

	public void GoToNextEnemyType()
	{
		if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			currentDefeatedEnemiesCounter = defeatedEnemiesCounters[enemyTypeIndex];
			currentEnemyTypePointsCounter = enemyTypePointsCounters[enemyTypeIndex];
			defeatedEnemies = defeatedEnemiesCount[enemyTypeIndex];
			scorePerEnemy = defeatedEnemiesData[enemyTypeIndex].score;
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
		}
		else
		{
			enemyTypeSwitch.ResetTimer();
		}
	}

	private void ResetTotalDefeatedEnemiesCounter() => totalDefeatedEnemiesCounter.text = string.Empty;
	private void SetHighScore() => highScoreCounter.text = gameData.highScore.ToString();
	private void SetPlayerOneScore() => playerOneScoreCounter.text = playerData.Score.ToString();
	private void RetrieveEnemiesData() => defeatedEnemiesData = playerData.DefeatedEnemies.Keys.ToArray<EnemyData>();
	private void RetrieveEnemiesCount() => defeatedEnemiesCount = playerData.DefeatedEnemies.Values.ToArray<int>();
	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;

	private void Start()
	{
		ResetTotalDefeatedEnemiesCounter();
		SetHighScore();
		SetPlayerOneScore();
		RetrieveEnemiesData();
		RetrieveEnemiesCount();
		BuildPointsRows();
		SetTotalTextPosition();
	}

	private void BuildPointsRows()
	{
		int amount = DefeatedEnemiesTypes();

		defeatedEnemiesCounters = new TextMeshProUGUI[amount];
		enemyTypePointsCounters = new TextMeshProUGUI[amount];

		for (int i = 0; i < amount; ++i)
		{
			int y = -80 - 16*i;
			
			CreateElement(pointsText, 64, y);
			CreateDefeatedEnemiesCounter(96, y, i);
			CreateElement(leftArrow, 112, y);
			CreateElement(enemyType, 0, y + 4);
			CreateEnemyTypePointsCounter(16, y, i);
		}
	}

	private void SetTotalTextPosition()
	{
		int offsetY = -16*DefeatedEnemiesTypes();
		
		totalText.anchoredPosition = new Vector2(totalText.anchoredPosition.x, totalText.anchoredPosition.y + offsetY);
		horizontalLine.anchoredPosition = new Vector2(horizontalLine.anchoredPosition.x, horizontalLine.anchoredPosition.y + offsetY);

		RectTransform tdec = totalDefeatedEnemiesCounter.GetComponent<RectTransform>();

		tdec.anchoredPosition = new Vector2(tdec.anchoredPosition.x, totalText.anchoredPosition.y);
	}

	private void CreateElement(GameObject element, float x, float y)
	{
		GameObject instance = Instantiate(element, parent);
		RectTransform rt = instance.GetComponent<RectTransform>();

		if(rt != null)
		{
			rt.anchoredPosition = new Vector2(x, y);
		}
	}

	private void CreateDefeatedEnemiesCounter(float x, float y, int index)
	{
		GameObject instance = Instantiate(defeatedEnemiesCounter, parent);
		RectTransform rt = instance.GetComponent<RectTransform>();

		if(rt != null)
		{
			rt.anchoredPosition = new Vector2(x, y);
		}

		TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();

		if(text != null)
		{
			defeatedEnemiesCounters[index] = text;
		}
	}

	private void CreateEnemyTypePointsCounter(float x, float y, int index)
	{
		GameObject instance = Instantiate(enemyTypePointsCounter, parent);
		RectTransform rt = instance.GetComponent<RectTransform>();

		if(rt != null)
		{
			rt.anchoredPosition = new Vector2(x, y);
		}

		TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();

		if(text != null)
		{
			enemyTypePointsCounters[index] = text;
		}
	}
}