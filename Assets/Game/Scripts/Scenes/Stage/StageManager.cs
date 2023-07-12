using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	public GameObject[] enemies;

	private GameObject[] enemySpawners;
	private int enemyIndex = 0;

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
			yield return new WaitForSeconds(5);

			ResetEnemySpawnersTimers();
		}
	}

	private void ResetEnemySpawnersTimers()
	{
		foreach (GameObject es in enemySpawners)
		{
			Timer timer = es.GetComponent<Timer>();

			timer.ResetTimer();
		}
	}
}