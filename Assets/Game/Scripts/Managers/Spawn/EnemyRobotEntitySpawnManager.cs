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
	private List<EnemyEntitySpawner> enemyEntitySpawners;
	private int currentEnemyEntitySpawnerIndex;
	private int currentEnemyEntityIndex;
	private int numberOfEnemiesToSpawn;

	public bool NoEnemiesLeft() => currentEnemyEntityIndex >= GetTotalNumberOfEnemies();
	public int GetTotalNumberOfEnemies() => stageEnemyTypesLoadingManager != null && stageEnemyTypesLoadingManager.EnemyPrefabs != null ? stageEnemyTypesLoadingManager.EnemyPrefabs.Length : 0;

	public void StartSpawn()
	{
		enemyEntitySpawners.ForEach(AssignEntityToSpawner);
		StartCoroutine(StartSpawningEntities());
	}

	private void Awake()
	{
		stageEnemyTypesLoadingManager = FindAnyObjectByType<StageEnemyTypesLoadingManager>();
		enemyEntitySpawners = FindObjectsByType<EnemyEntitySpawner>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList().OrderBy(enemyEntitySpawner => enemyEntitySpawner.GetOrdinalNumber()).ToList();
	}

	private void AssignEntityToSpawner(EnemyEntitySpawner enemyEntitySpawner)
	{
		if(enemyEntitySpawner == null)
		{
			return;
		}

		if(stageEnemyTypesLoadingManager != null)
		{
			if(stageEnemyTypesLoadingManager.EnemyPrefabs != null && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyPrefabs.Length)
			{
				enemyEntitySpawner.SetEntityPrefab(stageEnemyTypesLoadingManager.EnemyPrefabs[currentEnemyEntityIndex]);
			}
			
			if(stageEnemyTypesLoadingManager.EnemyTypes != null && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyTypes.Length)
			{
				enemyEntitySpawner.IsBonus = stageEnemyTypesLoadingManager.EnemyTypes[currentEnemyEntityIndex].IsBonus();
			}
		}
		
		++currentEnemyEntityIndex;

		StageManager.instance.uiManager.leftEnemyIconsManager.DestroyLeftEnemyIcon();
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
		while (enemyEntitySpawners != null && numberOfEnemiesToSpawn > 0 && stageEnemyTypesLoadingManager != null && stageEnemyTypesLoadingManager.EnemyPrefabs != null && currentEnemyEntityIndex < stageEnemyTypesLoadingManager.EnemyPrefabs.Length)
		{
			var currentEnemyEntitySpawner = enemyEntitySpawners[currentEnemyEntitySpawnerIndex];

			currentEnemyEntitySpawner.ResetTimer();
			OnSpawnerTimerReset(currentEnemyEntitySpawner);
		}
	}

	private void OnSpawnerTimerReset(EnemyEntitySpawner enemyEntitySpawner)
	{
		if(enemyEntitySpawners == null || enemyEntitySpawner == null)
		{
			return;
		}
		
		AssignEntityToSpawner(enemyEntitySpawner);

		--numberOfEnemiesToSpawn;
		currentEnemyEntitySpawnerIndex = (currentEnemyEntitySpawnerIndex + 1) % enemyEntitySpawners.Count;
	}
}