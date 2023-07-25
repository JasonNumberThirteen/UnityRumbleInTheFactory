using TMPro;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public PlayerData playerData;
	public RectTransform parent; 
	public GameObject pointsText, defeatedEnemiesCounter, leftArrow, enemyType, enemyTypePointsCounter;
	public RectTransform totalText, horizontalLine;
	public Timer enemyTypeSwitch, scoreCountTimer;
	public TextMeshProUGUI totalDefeatedEnemiesCounter;

	private int enemyTypeIndex, countedEnemies, totalCountedEnemies, enemyTypeScore;
	private TextMeshProUGUI[] defeatedEnemiesCounters, enemyTypePointsCounters;
	private TextMeshProUGUI currentDefeatedEnemiesCounter, currentEnemyTypePointsCounter;

	public void GoToNextEnemyType()
	{
		if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			currentDefeatedEnemiesCounter = defeatedEnemiesCounters[enemyTypeIndex];
			currentEnemyTypePointsCounter = enemyTypePointsCounters[enemyTypeIndex];
			++enemyTypeIndex;
			countedEnemies = enemyTypeScore = 0;
		}
		else if(totalDefeatedEnemiesCounter.text != totalCountedEnemies.ToString())
		{
			totalDefeatedEnemiesCounter.text = totalCountedEnemies.ToString();
		}
	}

	public void CountPoints()
	{
		if(countedEnemies < 20)
		{
			++countedEnemies;
			++totalCountedEnemies;
			enemyTypeScore += 100;
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
	private int DefeatedEnemiesTypes() => 4;

	private void Start()
	{
		ResetTotalDefeatedEnemiesCounter();
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