using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
	public GameObject[] enemies;
	public string spawnerTag;
	public int enemiesLimit = 3;
	public float spawnInterval = 2f;

	private GameObject[] spawners;
	private int enemyIndex, enemiesToSpawn, spawnerIndex;

	public bool NoEnemiesLeft() => enemyIndex >= enemies.Length;

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