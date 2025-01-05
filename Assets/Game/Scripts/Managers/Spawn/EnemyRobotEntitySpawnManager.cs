using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotEntitySpawnManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private string enemyTag;
	[SerializeField, Min(0.01f)] private float spawnInterval = 2f;

	private StageEnemyTypesLoadingManager stageEnemyTypesLoadingManager;
	private List<EnemyRobotEntitySpawner> enemyRobotEntitySpawners;
	private int currentEnemyRobotEntitySpawnerIndex;
	private int currentEnemyEntityIndex;
	private int numberOfEnemiesToSpawn;

	public bool NoEnemiesLeft() => currentEnemyEntityIndex >= GetTotalNumberOfEnemies();
	public int GetTotalNumberOfEnemies() => stageEnemyTypesLoadingManager != null && stageEnemyTypesLoadingManager.EnemyPrefabs != null ? stageEnemyTypesLoadingManager.EnemyPrefabs.Length : 0;

	public void StartSpawn()
	{
		enemyRobotEntitySpawners.ForEach(AssignEntityToSpawner);
		StartCoroutine(StartSpawningEntities());
	}

	private void Awake()
	{
		stageEnemyTypesLoadingManager = FindAnyObjectByType<StageEnemyTypesLoadingManager>();
		enemyRobotEntitySpawners = FindObjectsByType<EnemyRobotEntitySpawner>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList().OrderBy(enemyRobotEntitySpawner => enemyRobotEntitySpawner.GetOrdinalNumber()).ToList();
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
				enemyRobotEntitySpawner.IsBonus = stageEnemyTypesLoadingManager.EnemyTypes[currentEnemyEntityIndex].IsBonus();
			}
		}
		
		++currentEnemyEntityIndex;

		StageManager.instance.uiManager.leftEnemiesToSpawnImagesUIManager.DestroyLeftEnemyIcon();
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

			currentEnemyRobotEntitySpawner.ResetTimer();
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
}