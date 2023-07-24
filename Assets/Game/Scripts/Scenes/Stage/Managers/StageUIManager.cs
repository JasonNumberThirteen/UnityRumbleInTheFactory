using TMPro;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent, hud;
	public GameObject gainedPointsCounter, leftEnemyIcon, pauseText;
	public PlayerData playerData;
	public GameData gameData;
	public Counter playerOneLivesCounter, stageCounter;

	private GameObject[] leftEnemyIcons;
	private int leftEnemyIconIndex;

	public void ControlPauseTextDisplay() => pauseText.SetActive(StageManager.instance.State == StageManager.GameStates.PAUSED);

	public void UpdateCounters()
	{
		playerOneLivesCounter.SetTo(playerData.Lives);
		stageCounter.SetTo(gameData.stage);
	}

	public void RemoveLeftEnemyIcon()
	{
		if(--leftEnemyIconIndex < leftEnemyIcons.Length)
		{
			Destroy(leftEnemyIcons[leftEnemyIconIndex]);
		}
	}

	public void CreateGainedPointsCounter(Vector2 position, int points)
	{
		GameObject instance = Instantiate(gainedPointsCounter, parent.transform);
		RectTransform rt = instance.GetComponent<RectTransform>();
		TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();

		rt.anchoredPosition = GainedPointsCounterPosition(position);
		text.text = points.ToString();
	}
	
	private void Start() => CreateLeftEnemyIcons();
	private Vector2 GainedPointsCounterPosition(Vector2 position) => position*16;
	private int LeftEnemyIconX(int index) => 8*(index % 2);
	private int LeftEnemyIconY(int index) => -8*(index >> 1);
	private Vector2 LeftEnemyIconPosition(int index)
	{
		int offsetX = LeftEnemyIconX(index);
		int offsetY = LeftEnemyIconY(index);
		Vector2 initialPosition = new Vector2(-16, -16);
		Vector2 offset = new Vector2(offsetX, offsetY);
		
		return initialPosition + offset;
	}

	private int LeftEnemyIconsCount(int amountOfEnemies)
	{
		int limit = 20;
		
		return Mathf.Min(amountOfEnemies, limit);
	}

	private void CreateLeftEnemyIcons()
	{
		int amountOfEnemies = StageManager.instance.enemySpawnManager.enemies.Length;
		int amountOfIcons = LeftEnemyIconsCount(amountOfEnemies);

		leftEnemyIcons = new GameObject[amountOfIcons];
		leftEnemyIconIndex = amountOfEnemies;

		for (int i = 0; i < amountOfIcons; ++i)
		{
			GameObject instance = Instantiate(leftEnemyIcon, hud.transform);
			RectTransform rt = instance.GetComponent<RectTransform>();

			rt.anchoredPosition = LeftEnemyIconPosition(i);
			leftEnemyIcons[i] = instance;
		}
	}
}