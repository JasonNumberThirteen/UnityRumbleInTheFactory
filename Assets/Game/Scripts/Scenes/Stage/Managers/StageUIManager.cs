using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent, difficultyTier;
	public GameObject gainedPointsCounter, pauseText;
	public GameData gameData;
	public PlayerData[] playersData;
	public Counter[] playerLivesCounters;
	public Counter stageCounterText, stageCounterIcon;
	public LeftEnemyIconsManager leftEnemyIconsManager;

	public void ControlPauseTextDisplay() => pauseText.SetActive(StageManager.instance.stateManager.IsPaused());
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

	private void Start()
	{
		stageCounterText.SetTo(gameData.StageNumber);

		difficultyTier.sizeDelta = new Vector2(16*gameData.difficulty.CurrentTier(), 16);
	}
}