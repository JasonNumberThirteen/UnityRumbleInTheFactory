using TMPro;
using System.Linq;
using UnityEngine;

public class ScoreUIManager : UIManager
{
	public ScorePointsRowsBuilder pointsRowsBuilder;
	public PlayerRobotData playerData;
	public GameData gameData;
	public RectTransformPositionController totalTextMover, horizontalLineMover, totalDefeatedEnemiesCounterMover;
	public Timer enemyTypeSwitch, scoreCountTimer, sceneManagerTimer;
	public TextMeshProUGUI totalDefeatedEnemiesCounter;
	public AudioSource audioSource;

	private int enemyTypeIndex, countedEnemies, enemyTypeScore, defeatedEnemies, scorePerEnemy;
	private IntCounter currentDefeatedEnemiesIntCounter;
	private IntCounter currentEnemyTypePointsIntCounter;
	private EnemyRobotData[] defeatedEnemiesData;
	private int[] defeatedEnemiesCount;

	public void GoToNextEnemyType()
	{
		if(enemyTypeIndex < DefeatedEnemiesTypes())
		{
			currentDefeatedEnemiesIntCounter = pointsRowsBuilder.DefeatedEnemiesIntCounters[enemyTypeIndex];
			currentEnemyTypePointsIntCounter = pointsRowsBuilder.EnemyTypePointsIntCounters[enemyTypeIndex];
			defeatedEnemies = defeatedEnemiesCount[enemyTypeIndex];
			scorePerEnemy = defeatedEnemiesData[enemyTypeIndex].GetPointsForDefeat();
			++enemyTypeIndex;
			countedEnemies = enemyTypeScore = 0;
		}
		else if(!totalDefeatedEnemiesCounter.enabled)
		{
			SetTotalDefeatedEnemiesCounterEnabled(true);
			sceneManagerTimer.StartTimer();
		}
	}

	public void CountPoints()
	{
		if(countedEnemies < defeatedEnemies)
		{
			++countedEnemies;
			enemyTypeScore += scorePerEnemy;
			
			currentDefeatedEnemiesIntCounter.SetTo(countedEnemies);
			currentEnemyTypePointsIntCounter.SetTo(enemyTypeScore);
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
		defeatedEnemiesData = playerData.DefeatedEnemies.Keys.ToArray();
		defeatedEnemiesCount = playerData.DefeatedEnemies.Values.ToArray();
		
		SetTotalDefeatedEnemiesCounterEnabled(false);
		pointsRowsBuilder.SetDefeatedEnemiesSprites(defeatedEnemiesData.Select(e => e.GetDisplayInScoreSceneSprite()).ToArray());
		pointsRowsBuilder.BuildPointsRows();
		SetLastElementsPosition();
	}

	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;
	private void SetTotalDefeatedEnemiesCounterEnabled(bool enabled) => totalDefeatedEnemiesCounter.enabled = enabled;

	private void SetLastElementsPosition()
	{
		int offsetY = -16*DefeatedEnemiesTypes();
		
		totalTextMover.AddPositionY(offsetY);
		horizontalLineMover.AddPositionY(offsetY);
		totalDefeatedEnemiesCounterMover.SetPositionY(totalTextMover.GetPositionY());
	}
}