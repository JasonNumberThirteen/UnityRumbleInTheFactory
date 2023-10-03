using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent, hud, difficultyTier;
	public GameObject gainedPointsCounter, leftEnemyIcon, pauseText;
	public GameData gameData;
	public PlayerData[] playersData;
	public Counter[] playerLivesCounters;
	public Counter stageCounterText, stageCounterIcon;
	[Min(0)] public int leftEnemyIconsLimit = 20;

	private GameObject[] leftEnemyIcons;
	private int leftEnemyIconIndex;

	public void ControlPauseTextDisplay() => pauseText.SetActive(StageManager.instance.IsPaused());
	public void UpdateStageCounterIcon() => stageCounterIcon.SetTo(gameData.StageNumber);

	public void UpdateCounters()
	{
		UpdateLivesCounters();
		UpdateStageCounterIcon();
	}

	public void UpdateLivesCounters()
	{
		for (int i = 0; i < playersData.Length; ++i)
		{
			playerLivesCounters[i].SetTo(playersData[i].Lives);
		}
	}

	public void DestroyLeftEnemyIcon()
	{
		if(--leftEnemyIconIndex < leftEnemyIcons.Length)
		{
			Destroy(leftEnemyIcons[leftEnemyIconIndex]);
		}
	}

	public void InstantiateGainedPointsCounter(Vector2 position, int points)
	{
		GameObject instance = Instantiate(gainedPointsCounter, parent.transform);
		
		if(instance.TryGetComponent(out RectTransformMover rtm))
		{
			rtm.SetPosition(GainedPointsCounterPosition(position));
		}

		if(instance.TryGetComponent(out Counter counter))
		{
			counter.SetTo(points);
		}
	}
	
	private Vector2 GainedPointsCounterPosition(Vector2 position) => position*16;
	private int LeftEnemyIconsCount(int enemiesCount) => Mathf.Min(enemiesCount, leftEnemyIconsLimit);
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

	private void Start()
	{
		stageCounterText.SetTo(gameData.StageNumber);
		InstantiateLeftEnemyIcons();

		difficultyTier.sizeDelta = new Vector2(16*gameData.difficulty.CurrentTier(), 16);
	}

	private void InstantiateLeftEnemyIcons()
	{
		int enemiesCount = StageManager.instance.enemySpawnManager.EnemiesCount();
		int iconsCount = LeftEnemyIconsCount(enemiesCount);

		leftEnemyIcons = new GameObject[iconsCount];
		leftEnemyIconIndex = enemiesCount;

		for (int i = 0; i < iconsCount; ++i)
		{
			InstantiateLeftEnemyIcon(i);
		}
	}

	private void InstantiateLeftEnemyIcon(int index)
	{
		GameObject instance = Instantiate(leftEnemyIcon, hud.transform);
		
		if(instance.TryGetComponent(out RectTransformMover rtm))
		{
			rtm.SetPosition(LeftEnemyIconPosition(index));
		}

		leftEnemyIcons[index] = instance;
	}
}