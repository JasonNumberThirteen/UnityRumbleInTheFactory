using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
	public GameData gameData;
	public string enemyTag, spawnerTag;
	[Min(0.01f)] public float spawnInterval = 2f, bonusEnemyColorFadeTime = 1f;
	public GameObject[] bonuses;
	public Color bonusEnemyColor;
	public StageEnemyTypesLoadingManager enemyTypesReader;

	private GameObject[] spawners;
	private int enemyIndex, enemiesToSpawn, spawnerIndex;

	public bool NoEnemiesLeft() => enemyIndex >= EnemiesCount();
	public int EnemiesCount() => enemyTypesReader.EnemyPrefabs.Length;

	public void StartSpawn()
	{
		FindSpawners();
		AssignEnemiesToSpawners();
		StartCoroutine(SpawnEnemies());
	}

	private void FindSpawners() => spawners = GameObject.FindGameObjectsWithTag(spawnerTag);

	private void AssignEnemiesToSpawners()
	{
		foreach (GameObject spawner in spawners)
		{
			AssignEnemyToSpawner(spawner, OnEnemyAssignAtStart);
		}
	}

	private void AssignEnemyToSpawner(GameObject spawner, Action<EnemyEntitySpawner> OnAssign)
	{
		if(spawner.TryGetComponent(out EnemyEntitySpawner es))
		{
			OnAssign(es);
		}
	}

	private void OnEnemySpawnContinued(EnemyEntitySpawner es)
	{
		OnEnemyAssignAtStart(es);

		--enemiesToSpawn;
		spawnerIndex = (spawnerIndex + 1) % spawners.Length;
	}

	private void OnEnemyAssignAtStart(EnemyEntitySpawner es)
	{
		es.SetEntityPrefab(enemyTypesReader.EnemyPrefabs[enemyIndex]);

		es.IsBonus = enemyTypesReader.EnemyTypes[enemyIndex].IsBonus();
		++enemyIndex;

		StageManager.instance.uiManager.leftEnemyIconsManager.DestroyLeftEnemyIcon();
	}

	private IEnumerator SpawnEnemies()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);

			DetermineEnemiesToSpawn();
			ResetSpawnersTimers();
		}
	}

	private void DetermineEnemiesToSpawn()
	{
		int aliveEnemies = GameObject.FindGameObjectsWithTag(enemyTag).Length;
		int missingEnemies = gameData.GetDifficultyTierValue(tier => tier.GetEnemiesLimitAtOnce()) - aliveEnemies;
		
		enemiesToSpawn = Mathf.Max(0, missingEnemies);
	}

	private void ResetSpawnersTimers()
	{
		while (enemiesToSpawn > 0 && enemyIndex < enemyTypesReader.EnemyPrefabs.Length)
		{
			GameObject spawner = spawners[spawnerIndex];

			ResetSpawnerTimer(spawner);
			AssignEnemyToSpawner(spawner, OnEnemySpawnContinued);
		}
	}

	private void ResetSpawnerTimer(GameObject spawner)
	{
		if(spawner.TryGetComponent(out Timer timer))
		{
			timer.ResetTimer();
		}
	}
}