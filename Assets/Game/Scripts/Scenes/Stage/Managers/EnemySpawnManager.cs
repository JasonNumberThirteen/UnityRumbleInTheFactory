using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
	public EnemyData[] enemiesData;
	public GameData gameData;
	public string enemyTag, spawnerTag;
	[Min(0.01f)] public float spawnInterval = 2f;

	private GameObject[] enemies, spawners;
	private int enemyIndex, enemiesToSpawn, spawnerIndex;

	public bool NoEnemiesLeft() => enemyIndex >= EnemiesCount();
	public int EnemiesCount() => enemies.Length;

	public void StartSpawn()
	{
		FindSpawners();
		AssignEnemiesToSpawners();
		StartCoroutine(SpawnEnemies());
	}

	private void Awake() => AssignEnemiesFromCurrentStage();
	private void FindSpawners() => spawners = GameObject.FindGameObjectsWithTag(spawnerTag);

	private void AssignEnemiesFromCurrentStage()
	{
		int[] enemyIndexes = gameData.CurrentStage().enemies;
		int count = enemyIndexes.Length;
		
		enemies = new GameObject[count];

		for (int i = 0; i < count; ++i)
		{
			int index = enemyIndexes[i];
			
			enemies[i] = enemiesData[index].prefab;
		}
	}

	private void AssignEnemiesToSpawners()
	{
		foreach (GameObject spawner in spawners)
		{
			AssignEnemyToSpawner(spawner, OnEnemyAssignAtStart);
		}
	}

	private void AssignEnemyToSpawner(GameObject spawner, Action<EntitySpawner> OnAssign)
	{
		if(spawner.TryGetComponent(out EntitySpawner es))
		{
			OnAssign(es);
		}
	}

	private void OnEnemySpawnContinued(EntitySpawner es)
	{
		OnEnemyAssignAtStart(es);

		--enemiesToSpawn;
		spawnerIndex = (spawnerIndex + 1) % spawners.Length;
	}

	private void OnEnemyAssignAtStart(EntitySpawner es)
	{
		es.entity = enemies[enemyIndex++];

		StageManager.instance.uiManager.DestroyLeftEnemyIcon();
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
		int missingEnemies = gameData.difficulty.EnemiesLimit() - aliveEnemies;
		
		enemiesToSpawn = Mathf.Max(0, missingEnemies);
	}

	private void ResetSpawnersTimers()
	{
		while (enemiesToSpawn > 0 && enemyIndex < enemies.Length)
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