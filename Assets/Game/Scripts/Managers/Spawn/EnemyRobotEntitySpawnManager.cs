using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRobotEntitySpawnManager : MonoBehaviour
{
	public UnityEvent entityAssignedToSpawnerEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private string enemyTag;

	private StageEnemyTypesLoadingManager stageEnemyTypesLoadingManager;
	private StageSceneFlowManager stageSceneFlowManager;
	private StageStateManager stageStateManager;
	private StageEventsManager stageEventsManager;
	private List<EnemyRobotEntitySpawner> enemyRobotEntitySpawners;
	private int currentEnemyRobotEntitySpawnerIndex;
	private int currentEnemyEntityIndex;
	private int numberOfEnemiesToSpawn;
	private int numberOfDefeatedEnemies;
	private float spawnInterval;
	
	public int GetTotalNumberOfEnemies() => stageEnemyTypesLoadingManager != null && stageEnemyTypesLoadingManager.EnemyPrefabs != null ? stageEnemyTypesLoadingManager.EnemyPrefabs.Length : 0;

	private void Awake()
	{
		stageEnemyTypesLoadingManager = ObjectMethods.FindComponentOfType<StageEnemyTypesLoadingManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		enemyRobotEntitySpawners = ObjectMethods.FindComponentsOfType<EnemyRobotEntitySpawner>().OrderBy(enemyRobotEntitySpawner => enemyRobotEntitySpawner.GetOrdinalNumber()).Take(GetTotalNumberOfEnemies()).ToList();
		spawnInterval = gameData != null ? gameData.GetDifficultyTierValue(tier => tier.GetEnemySpawnDelay()) : 0f;

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageActivatedEvent.AddListener(StartSpawnIfPossible);
			}

			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.AddListener(OnEventReceived);
			}
		}
		else
		{
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageActivatedEvent.RemoveListener(StartSpawnIfPossible);
			}

			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.RemoveListener(OnEventReceived);
			}
		}
	}

	private void StartSpawnIfPossible()
	{
		SetStageAsWonIfNeeded();
		enemyRobotEntitySpawners.ForEach(enemyRobotEntitySpawner =>
		{
			enemyRobotEntitySpawner.StartTimer();
			AssignEntityToSpawner(enemyRobotEntitySpawner);
		});

		if(enemyRobotEntitySpawners.Count > 0)
		{
			StartCoroutine(StartSpawningEntities());
		}
	}

	private void OnEventReceived(StageEventType stageEventType, GameObject sender)
	{
		if(stageEventType != StageEventType.EnemyDestroyed)
		{
			return;
		}

		++numberOfDefeatedEnemies;

		SetStageAsWonIfNeeded();
	}

	private void SetStageAsWonIfNeeded()
	{
		if(stageStateManager != null && WonStage())
		{
			stageStateManager.SetStateTo(StageState.Won);
		}
	}

	private void AssignEntityToSpawner(EnemyRobotEntitySpawner enemyRobotEntitySpawner)
	{
		if(enemyRobotEntitySpawner == null)
		{
			return;
		}

		if(stageEnemyTypesLoadingManager != null)
		{
			if(stageEnemyTypesLoadingManager.EnemyPrefabs != null && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyPrefabs.Length)
			{
				enemyRobotEntitySpawner.SetEntityPrefab(stageEnemyTypesLoadingManager.EnemyPrefabs[currentEnemyEntityIndex]);
			}
			
			if(stageEnemyTypesLoadingManager.EnemyTypes != null && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyTypes.Length)
			{
				enemyRobotEntitySpawner.IsBonus = stageEnemyTypesLoadingManager.EnemyTypes[currentEnemyEntityIndex].IsBonus;
			}
		}
		
		++currentEnemyEntityIndex;

		entityAssignedToSpawnerEvent?.Invoke();
	}

	private IEnumerator StartSpawningEntities()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);

			DetermineNumberOfEnemiesToSpawn();
			StartSpawnersTimersIfPossible();
		}
	}

	private void DetermineNumberOfEnemiesToSpawn()
	{
		var numberOfAliveEnemies = GameObject.FindGameObjectsWithTag(enemyTag).Length;
		var enemiesLimitAtOnce = gameData != null ? gameData.GetDifficultyTierValue(tier => tier.GetEnemiesLimitAtOnce()) : 0;
		
		numberOfEnemiesToSpawn = Mathf.Max(0, enemiesLimitAtOnce - numberOfAliveEnemies);
	}

	private void StartSpawnersTimersIfPossible()
	{
		if(enemyRobotEntitySpawners == null || enemyRobotEntitySpawners.Count == 0 || stageEnemyTypesLoadingManager == null || stageEnemyTypesLoadingManager.EnemyPrefabs == null)
		{
			return;
		}

		var numberOfIterations = Mathf.Min(numberOfEnemiesToSpawn, enemyRobotEntitySpawners.Count);

		for (var i = 0; i < numberOfIterations && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyPrefabs.Length; ++i)
		{
			InitiateSpawner(enemyRobotEntitySpawners[currentEnemyRobotEntitySpawnerIndex]);
		}
	}

	private void InitiateSpawner(EnemyRobotEntitySpawner enemyRobotEntitySpawner)
	{
		enemyRobotEntitySpawner.StartTimer();
		OnSpawnerTimerReset(enemyRobotEntitySpawner);
	}

	private void OnSpawnerTimerReset(EnemyRobotEntitySpawner enemyRobotEntitySpawner)
	{
		if(enemyRobotEntitySpawners == null || enemyRobotEntitySpawner == null)
		{
			return;
		}
		
		AssignEntityToSpawner(enemyRobotEntitySpawner);

		--numberOfEnemiesToSpawn;
		currentEnemyRobotEntitySpawnerIndex = (currentEnemyRobotEntitySpawnerIndex + 1) % enemyRobotEntitySpawners.Count;
	}

	private bool WonStage()
	{
		var stageCanBeWon = stageStateManager == null || !stageStateManager.GameIsOver();
		var totalNumberOfEnemies = GetTotalNumberOfEnemies();
		
		return stageCanBeWon && numberOfDefeatedEnemies >= totalNumberOfEnemies && currentEnemyEntityIndex >= totalNumberOfEnemies;
	}
}