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
	public EnemyTypesReader enemyTypesReader;

	private GameObject[] spawners;
	private int enemyIndex, enemiesToSpawn, spawnerIndex;

	public bool NoEnemiesLeft() => enemyIndex >= EnemiesCount();
	public int EnemiesCount() => enemyTypesReader.Enemies.Length;

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

	private void AssignEnemyToSpawner(GameObject spawner, Action<EnemySpawner> OnAssign)
	{
		if(spawner.TryGetComponent(out EnemySpawner es))
		{
			OnAssign(es);
		}
	}

	private void OnEnemySpawnContinued(EnemySpawner es)
	{
		OnEnemyAssignAtStart(es);

		--enemiesToSpawn;
		spawnerIndex = (spawnerIndex + 1) % spawners.Length;
	}

	private void OnEnemyAssignAtStart(EnemySpawner es)
	{
		es.entity = enemyTypesReader.Enemies[enemyIndex];
		es.IsBonus = enemyTypesReader.EnemyTypes[enemyIndex].isBonus;
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
		int missingEnemies = gameData.difficulty.EnemiesLimit() - aliveEnemies;
		
		enemiesToSpawn = Mathf.Max(0, missingEnemies);
	}

	private void ResetSpawnersTimers()
	{
		while (enemiesToSpawn > 0 && enemyIndex < enemyTypesReader.Enemies.Length)
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