using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
	public EnemyData[] enemiesData;
	public GameData gameData;
	public string spawnerTag;
	public int enemiesLimit = 3;
	public float spawnInterval = 2f;

	private GameObject[] enemies;
	private GameObject[] spawners;
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
		int[] enemyIndexes = gameData.stages[gameData.StageNumber - 1].enemies;
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
			EntitySpawner es = spawner.GetComponent<EntitySpawner>();

			if(es != null)
			{
				es.entity = enemies[enemyIndex++];

				StageManager.instance.uiManager.RemoveLeftEnemyIcon();
			}
		}
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

	private void ResetSpawnersTimers()
	{
		while (enemiesToSpawn > 0 && enemyIndex < enemies.Length)
		{
			GameObject spawner = spawners[spawnerIndex];
			Timer timer = spawner.GetComponent<Timer>();
			EntitySpawner es = spawner.GetComponent<EntitySpawner>();

			timer.ResetTimer();

			if(es != null)
			{
				es.entity = enemies[enemyIndex++];
				--enemiesToSpawn;
				spawnerIndex = (spawnerIndex + 1) % spawners.Length;

				StageManager.instance.uiManager.RemoveLeftEnemyIcon();
			}
		}
	}

	private void DetermineEnemiesToSpawn()
	{
		int aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
		int missingEnemies = enemiesLimit - aliveEnemies;
		
		enemiesToSpawn = Mathf.Max(0, missingEnemies);
	}
}