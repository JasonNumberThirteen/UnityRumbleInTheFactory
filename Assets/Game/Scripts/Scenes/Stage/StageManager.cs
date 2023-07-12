using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	public GameObject[] enemies;
	public int enemiesLimit = 3;
	public float spawnInterval = 2f;

	private GameObject[] enemySpawners;
	private int enemyIndex = 0, enemiesToSpawn;

	public void StartSpawn()
	{
		enemySpawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");

		AssignEnemiesToSpawners();
		StartCoroutine(SpawnEnemies());
	}

	private void Awake() => CheckSingleton();

	private void CheckSingleton()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void AssignEnemiesToSpawners()
	{
		foreach (GameObject es in enemySpawners)
		{
			es.GetComponent<EntitySpawner>().entity = enemies[enemyIndex++];
		}
	}

	private IEnumerator SpawnEnemies()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);

			ResetEnemySpawnersTimers();
		}
	}

	private void ResetEnemySpawnersTimers()
	{
		enemiesToSpawn = Mathf.Max(0, enemiesLimit - GameObject.FindGameObjectsWithTag("Enemy").Length);
		
		foreach (GameObject es in enemySpawners)
		{
			if(enemiesToSpawn > 0)
			{
				Timer timer = es.GetComponent<Timer>();

				timer.ResetTimer();

				--enemiesToSpawn;
			}
		}
	}
}