using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
	public EnemyData[] enemiesData;
	public GameData gameData;
	public string enemyTag, spawnerTag;
	[Min(0.01f)] public float spawnInterval = 2f;

	public GameObject[] bonuses;
	public Color bonusEnemyTargetColor;
	[Min(0.01f)] public float bonusEnemyColorFadeTime = 1f;

	private GameObject[] enemies, spawners;
	private int enemyIndex, enemiesToSpawn, spawnerIndex;
	private EnemyType[] enemyTypes;

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
		enemyTypes = EnemiesTypes();

		int count = enemyTypes.Length;
		
		enemies = new GameObject[count];

		for (int i = 0; i < count; ++i)
		{
			int index = enemyTypes[i].index;
			
			enemies[i] = enemiesData[index].prefab;
		}
	}

	private EnemyType[] EnemiesTypes()
	{
		Stage stage = gameData.CurrentStage();
		int length = stage.enemies.Length;
		EnemyType[] types = new EnemyType[length];
		
		for (int i = 0; i < length; ++i)
		{
			string data = stage.enemies[i];
			int index = EnemyIndex(data);
			bool isBonusType = EnemyIsBonusType(data);
			
			types[i] = new EnemyType(index, isBonusType);
		}

		return types;
	}

	private int EnemyIndex(string data) => EnemyDataPointsToBonusType(data) ? int.Parse(data[1..]) : int.Parse(data);
	private bool EnemyIsBonusType(string data) => EnemyDataPointsToBonusType(data);
	private bool EnemyDataPointsToBonusType(string data) => data.StartsWith("B");

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
		es.entity = enemies[enemyIndex];
		es.IsBonus = enemyTypes[enemyIndex].isBonus;
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

public class EnemyType
{
	public int index;
	public bool isBonus;

	public EnemyType(int index, bool isBonus)
	{
		this.index = index;
		this.isBonus = isBonus;
	}
}