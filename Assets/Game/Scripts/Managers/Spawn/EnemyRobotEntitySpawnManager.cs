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
	[SerializeField, Min(0.01f)] private float spawnInterval = 2f;

	private StageEnemyTypesLoadingManager stageEnemyTypesLoadingManager;
	private StageSceneFlowManager stageSceneFlowManager;
	private StageStateManager stageStateManager;
	private List<EnemyRobotEntitySpawner> enemyRobotEntitySpawners;
	private int currentEnemyRobotEntitySpawnerIndex;
	private int currentEnemyEntityIndex;
	private int numberOfEnemiesToSpawn;
	private int numberOfDefeatedEnemies;
	
	public int GetTotalNumberOfEnemies() => stageEnemyTypesLoadingManager != null && stageEnemyTypesLoadingManager.EnemyPrefabs != null ? stageEnemyTypesLoadingManager.EnemyPrefabs.Length : 0;

	public void CountDefeatedEnemy()
	{
		++numberOfDefeatedEnemies;

		SetStageAsWonIfNeeded();
	}

	private void Awake()
	{
		stageEnemyTypesLoadingManager = ObjectMethods.FindComponentOfType<StageEnemyTypesLoadingManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		enemyRobotEntitySpawners = ObjectMethods.FindComponentsOfType<EnemyRobotEntitySpawner>().OrderBy(enemyRobotEntitySpawner => enemyRobotEntitySpawner.GetOrdinalNumber()).Take(GetTotalNumberOfEnemies()).ToList();
		
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
		}
		else
		{
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageActivatedEvent.RemoveListener(StartSpawnIfPossible);
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
			ResetSpawnersTimersIfPossible();
		}
	}

	private void DetermineNumberOfEnemiesToSpawn()
	{
		var numberOfAliveEnemies = GameObject.FindGameObjectsWithTag(enemyTag).Length;
		var enemiesLimitAtOnce = gameData != null ? gameData.GetDifficultyTierValue(tier => tier.GetEnemiesLimitAtOnce()) : 0;
		var numberOfEnemiesToSpawn = enemiesLimitAtOnce - numberOfAliveEnemies;
		
		this.numberOfEnemiesToSpawn = Mathf.Max(0, enemiesLimitAtOnce - numberOfAliveEnemies);
	}

	private void ResetSpawnersTimersIfPossible()
	{
		while (enemyRobotEntitySpawners != null && numberOfEnemiesToSpawn > 0 && stageEnemyTypesLoadingManager != null && stageEnemyTypesLoadingManager.EnemyPrefabs != null && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyPrefabs.Length)
		{
			var currentEnemyRobotEntitySpawner = enemyRobotEntitySpawners[currentEnemyRobotEntitySpawnerIndex];

			currentEnemyRobotEntitySpawner.StartTimer();
			OnSpawnerTimerReset(currentEnemyRobotEntitySpawner);
		}
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

	private bool WonStage() => DefeatedAllEnemies() && NoEnemiesLeft();
	private bool DefeatedAllEnemies() => numberOfDefeatedEnemies >= GetTotalNumberOfEnemies();
	private bool NoEnemiesLeft() => currentEnemyEntityIndex >= GetTotalNumberOfEnemies();
}