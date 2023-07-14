using TMPro;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent, hud;
	public GameObject gainedPointsCounter, leftEnemyIcon;
	public PlayerData playerData;
	public GameData gameData;
	public Counter playerOneLivesCounter, stageCounter;

	private GameObject[] leftEnemyIcons;
	private int leftEnemyIconIndex;

	public void CreateGainedPointsCounter(Vector2 position, int points)
	{
		GameObject instance = Instantiate(gainedPointsCounter, parent.transform);
		RectTransform rt = instance.GetComponent<RectTransform>();
		TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();
		Vector2 textPosition = new Vector2(position.x*16, position.y*16);

		rt.anchoredPosition = textPosition;
		text.text = points.ToString();
	}

	public void UpdateCounters()
	{
		playerOneLivesCounter.SetTo(playerData.Lives);
		stageCounter.SetTo(gameData.stage);
	}

	public void RemoveLeftEnemyIcon() => Destroy(leftEnemyIcons[leftEnemyIconIndex++]);
	
	private void Start() => CreateLeftEnemyIcons();

	private void CreateLeftEnemyIcons()
	{
		int limit = 20;
		int amount = Mathf.Min(StageManager.instance.enemySpawnManager.enemies.Length, limit);

		leftEnemyIcons = new GameObject[amount];
		leftEnemyIconIndex = 0;

		for (int i = amount - 1; i >= 0; --i)
		{
			GameObject instance = Instantiate(leftEnemyIcon, hud.transform);
			RectTransform rt = instance.GetComponent<RectTransform>();
			Vector2 iconPositionOffset = new Vector2(-16 + 8*(i % 2), -16 + 8*(i / 2) - 8*((limit - 1) / 2));

			rt.anchoredPosition = iconPositionOffset;
			leftEnemyIcons[i] = instance;
		}
	}
}