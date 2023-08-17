using TMPro;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

	private void ResetTotalDefeatedEnemiesCounter() => totalDefeatedEnemiesCounter.text = string.Empty;
	private void SetHighScore() => highScoreCounter.text = gameData.highScore.ToString();
	private void SetPlayerOneScore() => playerOneScoreCounter.text = playerData.Score.ToString();
	private void RetrieveEnemiesData() => defeatedEnemiesData = playerData.DefeatedEnemies.Keys.ToArray();
	private void RetrieveEnemiesCount() => defeatedEnemiesCount = playerData.DefeatedEnemies.Values.ToArray();
	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;

	private void BuildPointsRows()
	{
		int amount = DefeatedEnemiesTypes();

		defeatedEnemiesCounters = new TextMeshProUGUI[amount];
		enemyTypePointsCounters = new TextMeshProUGUI[amount];

		for (int i = 0; i < amount; ++i)
		{
			int y = -80 - 16*i;
			
			InstantiateElement(pointsText, new Vector2(64, y));
			InstantiateElement(defeatedEnemiesCounter, new Vector2(96, y), i, OnDefeatedEnemiesCounterInstantiate);
			InstantiateElement(leftArrow, new Vector2(112, y));
			InstantiateElement(enemyType, new Vector2(0, y + 4), i, OnEnemyTypeSpriteInstantiate);
			InstantiateElement(enemyTypePointsCounter, new Vector2(16, y), i, OnEnemyTypePointsCounterInstantiate);
		}
	}

	private GameObject InstantiateElement(GameObject element, Vector2 position)
	{
		GameObject instance = Instantiate(element, parent);
		
		if(instance.TryGetComponent(out RectTransform rt))
		{
			rt.anchoredPosition = position;
		}

		return instance;
	}

	private GameObject InstantiateElement(GameObject element, Vector2 position, int index, Action<GameObject, int> onInstantiate)
	{
		GameObject instance = InstantiateElement(element, position);

		onInstantiate(instance, index);

		return instance;
	}

	private void OnDefeatedEnemiesCounterInstantiate(GameObject instance, int index)
	{
		if(instance.TryGetComponent(out TextMeshProUGUI text))
		{
			defeatedEnemiesCounters[index] = text;
		}
	}

	private void OnEnemyTypeSpriteInstantiate(GameObject instance, int index)
	{
		if(instance.TryGetComponent(out Image image))
		{
			image.sprite = defeatedEnemiesData[index].sprite;
		}
	}

	private void OnEnemyTypePointsCounterInstantiate(GameObject instance, int index)
	{
		if(instance.TryGetComponent(out TextMeshProUGUI text))
		{
			enemyTypePointsCounters[index] = text;
		}
	}

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