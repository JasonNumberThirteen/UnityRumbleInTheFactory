using TMPro;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public PlayerData playerData;
	public RectTransform parent; 
	public GameObject pointsText, defeatedEnemiesCounter, leftArrow, enemyType;
	public RectTransform totalText, horizontalLine;
	public Timer enemyTypeSwitch, scoreCountTimer;

	private int enemyTypeIndex, countedEnemies;
	private TextMeshProUGUI[] defeatedEnemiesCounters;
	private TextMeshProUGUI currentDefeatedEnemiesCounter;

	public void GoToNextEnemyType()
	{
		if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			currentDefeatedEnemiesCounter = defeatedEnemiesCounters[enemyTypeIndex++];
			countedEnemies = 0;
		}
	}

	public void CountPoints()
	{
		if(++countedEnemies <= 20)
		{
			currentDefeatedEnemiesCounter.text = countedEnemies.ToString();

			scoreCountTimer.ResetTimer();
		}
		else if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			enemyTypeSwitch.ResetTimer();
		}
	}

	private int DefeatedEnemiesTypes() => 4;

	private void Start()
	{
		BuildPointsRows();
		SetTotalTextPosition();
	}

	private void BuildPointsRows()
	{
		int amount = DefeatedEnemiesTypes();

		defeatedEnemiesCounters = new TextMeshProUGUI[amount];

		for (int i = 0; i < amount; ++i)
		{
			int y = -80 - 16*i;
			
			CreateElement(pointsText, 64, y);
			CreateDefeatedEnemiesCounter(96, y, i);
			CreateElement(leftArrow, 112, y);
			CreateElement(enemyType, 0, y + 4);
		}
	}

	private void SetTotalTextPosition()
	{
		int offsetY = -16*DefeatedEnemiesTypes();
		
		totalText.anchoredPosition = new Vector2(totalText.anchoredPosition.x, totalText.anchoredPosition.y + offsetY);
		horizontalLine.anchoredPosition = new Vector2(horizontalLine.anchoredPosition.x, horizontalLine.anchoredPosition.y + offsetY);
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
}