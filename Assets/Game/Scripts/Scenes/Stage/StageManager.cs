using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;

	private GameObject[] enemySpawners;

	private void Awake() => CheckSingleton();

	private void Start()
	{
		enemySpawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");

		StartCoroutine(SpawnEnemies());
	}

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

	private IEnumerator SpawnEnemies()
	{
		while (true)
		{
			yield return new WaitForSeconds(5);

			foreach (GameObject es in enemySpawners)
			{
				Timer timer = es.GetComponent<Timer>();

				timer.ResetTimer();
			}
		}
	}
}