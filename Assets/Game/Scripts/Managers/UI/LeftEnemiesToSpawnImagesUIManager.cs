using UnityEngine;

public class LeftEnemiesToSpawnImagesUIManager : UIManager
{
	[SerializeField] private GameObject leftEnemyToSpawnImageUIPrefab;
	[SerializeField, Min(0)] private int maxIconsLimit = 20;
	
	private LeftEnemiesToSpawnPanelUI leftEnemiesToSpawnPanelUI;
	private EnemyRobotEntitySpawnManager enemyRobotEntitySpawnManager;
	private GameObject[] iconGOs;
	private int currentIconIndex;

	private void Awake()
	{
		leftEnemiesToSpawnPanelUI = ObjectMethods.FindComponentOfType<LeftEnemiesToSpawnPanelUI>();
		enemyRobotEntitySpawnManager = ObjectMethods.FindComponentOfType<EnemyRobotEntitySpawnManager>();

		RegisterToListeners(true);
	}
	
	private void Start()
	{
		var numberOfEnemies = enemyRobotEntitySpawnManager != null ? enemyRobotEntitySpawnManager.GetTotalNumberOfEnemies() : 0;
		var numberOfIcons = GetNumberOfIconsToSpawn(numberOfEnemies);

		iconGOs = new GameObject[numberOfIcons];
		currentIconIndex = numberOfEnemies;

		for (int i = 0; i < numberOfIcons; ++i)
		{
			iconGOs[i] = GetIconInstance();
		}
	}

	private GameObject GetIconInstance()
	{
		var transform = leftEnemiesToSpawnPanelUI != null ? leftEnemiesToSpawnPanelUI.transform : null;
		
		return Instantiate(leftEnemyToSpawnImageUIPrefab, transform);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(enemyRobotEntitySpawnManager != null)
			{
				enemyRobotEntitySpawnManager.entityAssignedToSpawnerEvent.AddListener(OnEntityAssignedToSpawner);
			}
		}
		else
		{
			if(enemyRobotEntitySpawnManager != null)
			{
				enemyRobotEntitySpawnManager.entityAssignedToSpawnerEvent.RemoveListener(OnEntityAssignedToSpawner);
			}
		}
	}

	private void OnEntityAssignedToSpawner()
	{
		if(--currentIconIndex < iconGOs.Length)
		{
			Destroy(iconGOs[currentIconIndex]);
		}
	}

	private int GetNumberOfIconsToSpawn(int numberOfEnemies) => Mathf.Min(numberOfEnemies, maxIconsLimit);
}